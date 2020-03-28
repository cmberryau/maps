using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Mono.Data.Sqlite;

namespace Maps.Data.Sqlite
{
    /// <summary>
    /// Responsible for parsing database types in the context of sqlite
    /// </summary>
    /// <typeparam name="TKey">The default key type for the db table</typeparam>
    /// <typeparam name="TValue">The value type for the db table</typeparam>
    public class SqliteDbTypeParser<TKey, TValue> : DbTypeParser<TKey, TValue>
    {
        /// <summary>
        /// The sqlite string to create a table
        /// </summary>
        public string CreateTable
        {
            get;
            private set;
        }

        /// <summary>
        /// The sqlite string to select a row
        /// </summary>
        public string SelectRow
        {
            get;
            private set;
        }

        /// <summary>
        /// The sqlite string to select a key
        /// </summary>
        public string SelectKey
        {
            get;
            private set;
        }

        /// <summary>
        /// The sqlite string to insert a row
        /// </summary>
        public string InsertRow
        {
            get;
            private set;
        }

        /// <summary>
        /// The sqlite string to select the count
        /// </summary>
        public string SelectCount
        {
            get;
            private set;
        }

        private static readonly IDictionary<Type, string> TypeSnippetMap = new Dictionary<Type, string>
        {
            {typeof(long), "INTEGER NOT NULL"},
            {typeof(bool), "INTEGER NOT NULL"},
            {typeof(long?), "INTEGER"},
            {typeof(bool?), "INTEGER"},
            {typeof(string), "TEXT"},
            {typeof(byte[]), "BLOB"},
            {typeof(Bitmap), "BLOB"},
        };

        private const string ParamSnippet = @"@param";
        private const int ByteBufferSize = 1024;

        // create table snippets
        private const string CreateTableSnippet = "CREATE TABLE `{0}` (" +
                                                      "{1}" +
                                                      "{2}" +
                                                  ");";
        private const string TableMemberSnippet = "`{0}` ";
        private const string PrimaryKeySnippet = "PRIMARY KEY({0})";
        private const string UniqueSnippet = " UNIQUE";

        // select row snippets
        private const string SelectRowSnippet = "SELECT " +
                                                    "* " +
                                                "FROM " +
                                                    "`{0}` " +
                                                "WHERE `{1}` IN ({2});";

        // select key snippets
        private const string SelectKeySnippet = "SELECT " +
                                                    "`{0}` " +
                                                "FROM " +
                                                    "`{1}` " +
                                                "WHERE " +
                                                    "{2};";

        private const string AndSnippet = " AND ";
        private const string WhereColumnSnippet = "`{0}` IS {1}";

        // insert row snippets
        private const string InsertRowSnippet = "REPLACE INTO " +
                                                    "`{0}` ({1}) " +
                                                "VALUES {2};";

        // select count snippet
        private const string SelectCountSnippet = "SELECT " +
                                                      "COUNT(*) " +
                                                  "FROM " +
                                                      "(SELECT * FROM {0})";

        private readonly string _table;
        private readonly byte[] _byteBuffer;

        /// <summary>
        /// Initializes a new instance of SqliteDbTableOperator
        /// </summary>
        /// <param name="table">The table name</param>
        public SqliteDbTypeParser(string table)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentException(nameof(table));
            }

            _table = table;
            _byteBuffer = new byte[ByteBufferSize];
        }

        /// <inheritdoc />
        public override void Parse()
        {
            base.Parse();

            CreateCreateTableString();
            CreateSelectRowString();
            CreateSelectKeyString();
            CreateInsertRowString();
            CreateSelectCountString();
        }

        /// <summary>
        /// Creats a SELECT COUNT command
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <returns>The prepared command</returns>
        public SqliteCommand SelectCountCommand(SqliteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var command = new SqliteCommand(string.Format(SelectCountSnippet, _table), connection);

            return command;
        }

        /// <summary>
        /// Creates a batch REPLACE INTO command
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <param name="count">The number to batch</param>
        /// <returns>Returns the prepared command</returns>
        public SqliteCommand BatchReplaceCommand(SqliteConnection connection, int count)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            // create the columns snippet
            var columnsSb = new StringBuilder();
            for (var i = 0; i < ColumnCount - 1; ++i)
            {
                columnsSb.Append($"{Names[i]}, ");
            }
            columnsSb.Append($"{Names[ColumnCount - 1]}");

            // create the values snippet
            var valuesSb = new StringBuilder();
            for (var i = 0; i < count - 1; i++)
            {
                valuesSb.Append(@"(");
                for (var j = 0; j < ColumnCount - 1; ++j)
                {
                    valuesSb.Append($"@{Names[j]}{i}, ");
                }

                valuesSb.Append($"@{Names[ColumnCount - 1]}{i}");
                valuesSb.Append(@"), ");
            }

            valuesSb.Append(@"(");
            for (var j = 0; j < ColumnCount - 1; ++j)
            {
                valuesSb.Append($"@{Names[j]}{count - 1}, ");
            }

            valuesSb.Append($"@{Names[ColumnCount - 1]}{count - 1}");
            valuesSb.Append(@")");

            // form the sql command string
            var sql = string.Format(InsertRowSnippet, _table, columnsSb, valuesSb);
            // create the command
            var command = new SqliteCommand(sql, connection);
            // add the parameters to the command
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < ColumnCount; ++j)
                {
                    command.Parameters.Add($"@{Names[j]}{i}", SqliteTypeMap.DbTypeFor(Types[j]));
                }
            }

            return command;
        }

        /// <summary>
        /// Fills a REPLACE INTO command's parameters
        /// </summary>
        /// <param name="command">The REPLACE INTO command</param>
        /// <param name="keys">The keys to fill the command parameters with</param>
        /// <param name="values">The values to fill the command parameters with</param>
        /// <param name="count">The number of rows to set</param>
        public void SetReplaceCommandParameters(SqliteCommand command, IList<TKey> keys, 
            IList<TValue> values, int count)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            // validate that the keys and values are the same length
            if (count > values.Count || count > keys.Count)
            {
                throw new ArgumentException($"Exceeds keys and or values count", nameof(keys));
            }

            // validate that the command has the correct number of parameters
            if (command.Parameters.Count / count != ColumnCount)
            {
                throw new ArgumentException("Invalid number of parameters in command", nameof(command));
            }

            // push the parameters onto the command
            for (int i = 0, k = -1; i < count; ++i)
            {
                // push each previously discovered member
                if (DbStoreContract)
                {
                    for (var j = 0; j < ColumnCount; ++j)
                    {
                        object value;
                        if (GeneratedPrimaryKey)
                        {
                            if (j == 0)
                            {
                                // use the key directly if we are using a generated primary key
                                value = keys[i];
                            }
                            else
                            {
                                // shift j back one when we generated the primary key
                                value = Fields[j - 1].GetValue(values[i]);
                            }
                        }
                        else
                        {
                            value = Fields[j].GetValue(values[i]);
                        }
                        
                        command.Parameters[++k].Value = value;
                    }
                }   
                else
                {
                    command.Parameters[++k].Value = keys[i];

                    if (Types[1] == typeof(Bitmap))
                    {
                        if (values[i] is Bitmap bitmap)
                        {
                            using (var stream = new MemoryStream())
                            {
                                bitmap.Save(stream, ImageFormat.Png);
                                command.Parameters[++k].Value = stream.ToArray();
                            }
                        }
                    }
                    else
                    {
                        command.Parameters[++k].Value = values[i];
                    }
                }
            }
        }

        /// <summary>
        /// Creates a SELECT command for a single row
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <returns>A SELECT command for a single row</returns>
        public SqliteCommand SelectRowCommand(SqliteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var sql = string.Format(SelectRowSnippet, _table, Names[PrimaryKeyIndex], ParamSnippet);

            var command = new SqliteCommand(sql, connection);
            command.Parameters.Add(ParamSnippet, SqliteTypeMap.DbTypeFor(Types[PrimaryKeyIndex]));

            return command;
        }

        /// <summary>
        /// Creates a SELECT command for a single row
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <param name="count">The number of rows to select</param>
        /// <returns>A SELECT command for a single row</returns>
        public SqliteCommand SelectRowCommand(SqliteConnection connection, int count)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var paramsb = new StringBuilder();

            for (var i = 0; i < count - 1; ++i)
            {
                paramsb.Append($"{ParamSnippet}{i}, ");
            }

            paramsb.Append($"{ParamSnippet}{count - 1}");

            var sql = string.Format(SelectRowSnippet, _table, Names[PrimaryKeyIndex], paramsb);
            var command = new SqliteCommand(sql, connection);

            for (var i = 0; i < count; ++i)
            {
                command.Parameters.Add($"{ParamSnippet}{i}", SqliteTypeMap.DbTypeFor(Types[PrimaryKeyIndex]));
            }

            return command;
        }

        /// <summary>
        /// Fills the parameters of a SELECT command for a single row
        /// </summary>
        /// <param name="command">The SELECT command</param>
        /// <param name="key">The key to use as a parameter</param>
        public void SetSelectRowParameters(SqliteCommand command, TKey key)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (command.Parameters.Count > 1)
            {
                throw new ArgumentException("Should have only one parameter", nameof(command));
            }

            command.Parameters[0].Value = key;
        }

        /// <summary>
        /// Fills the parameters of a SELECT command for a single row
        /// </summary>
        /// <param name="command">The SELECT command</param>
        /// <param name="keys">The keys to use as parameters</param>
        public void SetSelectRowParameters(SqliteCommand command, IList<TKey> keys)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (command.Parameters.Count != keys.Count)
            {
                throw new ArgumentException("Mismatching number of command parameters and keys",
                    nameof(command));
            }

            for (var i = 0; i < keys.Count; ++i)
            {
                command.Parameters[i].Value = keys[i];
            }
        }

        /// <summary>
        /// Parses a single line from a SELECT row command
        /// </summary>
        /// <param name="reader">The reader to parse</param>
        /// <returns>The value for the line</returns>
        public TValue ParseSelectRowReaderValue(SqliteDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            TValue result;

            // create an instance
            if (DbStoreContract)
            {
                result = ValueConstructor();
                // if its a db store instance, fill it's members
                for (var i = 0; i < ColumnCount; ++i)
                {
                    var value = GetReaderValue(reader, i);
                    if (GeneratedPrimaryKey)
                    {
                        // skip the first if we generated the primary key
                        if (i == 0)
                        {
                            continue;
                        }

                        // shift back one in fields if we generated the primary key
                        Fields[i - 1].SetValue(result, value);
                    }
                    else
                    {
                        Fields[i].SetValue(result, value);
                    }
                }
            }
            else
            {
                // if it's not, just cast the second column
                result = (TValue)GetReaderValue(reader, 1);
            }

            return result;
        }

        /// <summary>
        /// Parses a single line from a SELECT row command
        /// </summary>
        /// <param name="reader">The reader to parse</param>
        /// <returns>The value for the line</returns>
        public TKey ParseSelectRowReaderKey(SqliteDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            TKey result;

            // create an instance
            if (DbStoreContract)
            {
                result = (TKey)GetReaderValue(reader, PrimaryKeyIndex);
            }
            else
            {
                // if it's not, just cast the second column
                result = (TKey)GetReaderValue(reader, 0);
            }

            return result;
        }

        /// <summary>
        /// Creates a SELECT command for a single key
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <returns>A SELECT sqlite command</returns>
        public SqliteCommand SelectKeyCommand(SqliteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var command = new SqliteCommand(SelectKey, connection);

            for (int i = 0, j = -1; i < ColumnCount; ++i)
            {
                // skip the primary key
                if (i != PrimaryKeyIndex)
                {
                    command.Parameters.Add($"{ParamSnippet}{++j}", SqliteTypeMap.DbTypeFor(Types[i]));
                }
            }

            return command;
        }

        /// <summary>
        /// Sets the parameters for a single SELECT key command
        /// </summary>
        /// <param name="command">The SELECT key command</param>
        /// <param name="value">The value to use as a parameter</param>
        public void SetSelectKeyParameters(SqliteCommand command, TValue value)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (DbStoreContract)
            {
                for (int i = 0, j = -1; i < ColumnCount; ++i)
                {
                    // skip the primary key
                    if (i != PrimaryKeyIndex)
                    {
                        if (GeneratedPrimaryKey)
                        {
                            command.Parameters[++j].Value = Fields[i - 1].GetValue(value);
                        }
                        else
                        {
                            command.Parameters[++j].Value = Fields[i].GetValue(value);
                        }
                    }
                }
            }
            else
            {
                if (Types[1] == typeof(Bitmap))
                {
                    if (value is Bitmap bitmap)
                    {
                        using (var stream = new MemoryStream())
                        {
                            bitmap.Save(stream, ImageFormat.Png);
                            command.Parameters[0].Value = stream.ToArray();
                        }
                    }
                }
                else
                {
                    command.Parameters[0].Value = value;
                }
            }
        }

        /// <summary>
        /// Parses a single line from a SELECT key command reader
        /// </summary>
        /// <param name="reader">The reader to parse a line from</param>
        /// <returns>The key read</returns>
        public TKey ParseSelectKeyReader(SqliteDataReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return (TKey)GetReaderValue(reader, PrimaryKeyIndex);
        }

        /// <inheritdoc />
        protected override bool ValidateTypeIsStorable(Type type)
        {
            if (type == typeof(long))
            {

            }
            else if (type == typeof(long?))
            {

            }
            else if (type == typeof(bool))
            {

            }
            else if (type == typeof(bool?))
            {

            }
            else if (type == typeof(string))
            {

            }
            else if (type == typeof(byte[]))
            {

            }
            else if (type == typeof(Bitmap))
            {

            }
            else
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected override bool ValidateTypeCanBePrimaryKey(Type type)
        {
            if (type == typeof(long))
            {

            }
            else if (type == typeof(string))
            {

            }
            else
            {
                return false;
            }

            return true;
        }

        private void CreateSelectRowString()
        {
            SelectRow = string.Format(SelectRowSnippet, _table, 
                Names[PrimaryKeyIndex], "{0}");
        }

        private void CreateSelectKeyString()
        {
            var membersDefinition = new StringBuilder();

            // we skip the final column if its the primary key
            var skipFinalColumn = Names.Count - 1 == PrimaryKeyIndex;

            var j = -1;
            for (var i = 0; i < ColumnCount - 1; i++)
            {
                // we skip the column if its the primary key
                if (i != PrimaryKeyIndex)
                {
                    membersDefinition.Append(string.Format(WhereColumnSnippet, Names[i], $"{ParamSnippet}{++j}"));

                    // at second last column
                    if (i == ColumnCount - 2)
                    {
                        // are we skipping the last column
                        if (!skipFinalColumn)
                        {
                            membersDefinition.Append(AndSnippet);
                        }
                    }
                    else
                    {
                        membersDefinition.Append(AndSnippet);
                    }
                }
            }

            // if not skipping the final column, add it to the string
            if (!skipFinalColumn)
            {
                membersDefinition.Append(string.Format(WhereColumnSnippet, Names[Names.Count - 1], $"{ParamSnippet}{++j}"));
            }

            // set the string
            SelectKey = string.Format(SelectKeySnippet, Names[PrimaryKeyIndex], _table, membersDefinition);
        }

        private void CreateInsertRowString()
        {
            var membersDefinition = new StringBuilder();
            var valuesDefinition = new StringBuilder();

            for (var i = 0; i < Names.Count - 1; i++)
            {
                membersDefinition.Append($"`{Names[i]}`,");
                valuesDefinition.Append($"{{{i}}},");
            }

            membersDefinition.Append($"`{Names[Names.Count - 1]}`");
            valuesDefinition.Append($"{{{Names.Count - 1}}}");

            InsertRow = string.Format(InsertRowSnippet, _table, membersDefinition,
                valuesDefinition);
        }

        private void CreateCreateTableString()
        {
            var primaryKeys = new StringBuilder();
            var primaryKeysCount = 0;
            var membersDefinition = new StringBuilder();

            for (var i = 0; i < Names.Count; i++)
            {
                var memberDefinition = new StringBuilder(string.Format(TableMemberSnippet, Names[i]));
                memberDefinition.Append(TypeSnippetMap[Types[i]]);

                if (PrimaryKeys[i])
                {
                    if (Unique[i])
                    {
                        memberDefinition.Append(UniqueSnippet);
                    }

                    primaryKeys.Append($"`{Names[i]}`");

                    if (PrimaryKeyCount > 0 && ++primaryKeysCount < PrimaryKeyCount)
                    {
                        primaryKeys.Append(", ");
                    }
                }

                memberDefinition.Append(", ");
                membersDefinition.Append(memberDefinition);
            }

            var primaryKeysDefinition = string.Format(PrimaryKeySnippet, primaryKeys);

            CreateTable = string.Format(CreateTableSnippet, _table, membersDefinition,
                primaryKeysDefinition);
        }

        private object GetReaderValue(SqliteDataReader reader, int ordinal)
        {
            var type = Types[ordinal];
            object value;

            if (type == typeof(long))
            {
                value = reader.GetInt64(ordinal);
            }
            else if (type == typeof(long?))
            {
                if (!reader.IsDBNull(ordinal))
                {
                    value = reader.GetInt64(ordinal);
                }
                else
                {
                    value = null;
                }
            }
            else if (type == typeof(bool))
            {
                value = reader.GetBoolean(ordinal);
            }
            else if (type == typeof(bool?))
            {
                if (!reader.IsDBNull(ordinal))
                {
                    value = reader.GetBoolean(ordinal);
                }
                else
                {
                    value = null;
                }
            }
            else if (type == typeof(string))
            {
                if (!reader.IsDBNull(ordinal))
                {
                    value = reader.GetString(ordinal);
                }
                else
                {
                    value = null;
                }
            }
            else if (type == typeof(byte[]) || type == typeof(Bitmap))
            {
                if (!reader.IsDBNull(ordinal))
                {
                    // open a memory stream
                    using (var stream = new MemoryStream())
                    {
                        long bytesRead, totalBytes = 0L;
                        // get the bytes from the reader
                        while ((bytesRead = reader.GetBytes(ordinal, totalBytes, _byteBuffer, 0, ByteBufferSize)) > 0L)
                        {
                            stream.Write(_byteBuffer, 0, (int)bytesRead);
                            totalBytes += bytesRead;
                        }

                        // if we got bytes and we're dealing with a bitmap, make one
                        if (type == typeof(Bitmap) && totalBytes > 0L)
                        {
                            value = new Bitmap(stream);
                        }
                        else
                        {
                            value = stream.ToArray();
                        }
                    }
                }
                else
                {
                    value = null;
                }
            }
            else
            {
                throw new NotSupportedException();
            }

            return value;
        }

        private void CreateSelectCountString()
        {
            SelectCount = string.Format(SelectCountSnippet, ParamSnippet);
        }
    }
}