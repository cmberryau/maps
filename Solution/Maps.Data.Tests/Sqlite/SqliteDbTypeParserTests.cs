using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Maps.Data.Sqlite;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Maps.Data.Tests.Sqlite
{
    [TestFixture]
    public class SqliteDbTypeParserTests
    {
        [DbStoreContract]
        private class TestDbStorable
        {
            [DbStoreMember(0, PrimaryKey = true)]
            public long Int64TestMember;

            [DbStoreMember(1)]
            public long? NullableInt64TestMember;

            [DbStoreMember(2)]
            public bool BooleanTestMember;

            [DbStoreMember(3)]
            public bool? NullableBooleanTestMember;

            [DbStoreMember(4)]
            public string StringTestMember;

            public TestDbStorable(int firstMember)
            {
                Int64TestMember = firstMember;
            }

            // private constructor required for DbStoreContract
            private TestDbStorable()
            {

            }
        }

        [Test]
        public void TestParseMethod()
        {
            var a = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            a.Parse();

            var b = new SqliteDbTypeParser<long, string>("string_table");
            b.Parse();
        }

        [Test]
        public void TestBatchReplaceIntoCommandMethodDbStorable()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var command = parser.BatchReplaceCommand(conn, 2);
            command.Parameters[0].Value = 1;
            command.Parameters[1].Value = 2;
            command.Parameters[2].Value = 3;
            command.Parameters[3].Value = 4;
            command.Parameters[4].Value = "test_string_0";

            command.Parameters[5].Value = 5;
            command.Parameters[6].Value = 6;
            command.Parameters[7].Value = 7;
            command.Parameters[8].Value = 8;
            command.Parameters[9].Value = "test_string_1";

            command.ExecuteNonQuery();
        }

        [Test]
        public void TestSetCommandParametersMethodDbStoreContract()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var command = parser.BatchReplaceCommand(conn, 1);
            var keys = new List<long>
            {
                0
            };
            var values = new List<TestDbStorable>
            {
                new TestDbStorable(0)
            };
            parser.SetReplaceCommandParameters(command, keys, values, 1);
            command.ExecuteNonQuery();
        }

        [Test]
        public void TestSetCommandParametersMethodPrimitiveLong()
        {
            var parser = new SqliteDbTypeParser<long, long>("long_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var command = parser.BatchReplaceCommand(conn, 1);
            var keys = new List<long>
            {
                0
            };
            var values = new List<long>
            {
                1
            };
            parser.SetReplaceCommandParameters(command, keys, values, 1);
            command.ExecuteNonQuery();
        }

        [Test]
        public void TestSetCommandParametersMethodPrimitiveString()
        {
            var parser = new SqliteDbTypeParser<long, string>("string_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var command = parser.BatchReplaceCommand(conn, 1);
            var keys = new List<long>
            {
                0
            };
            var values = new List<string>
            {
                "test_string"
            };
            parser.SetReplaceCommandParameters(command, keys, values, 1);
            command.ExecuteNonQuery();
        }

        [Test]
        public void TestSetCommandParametersMethodBitmap()
        {
            var parser = new SqliteDbTypeParser<long, Bitmap>("bitmap_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var command = parser.BatchReplaceCommand(conn, 1);
            var keys = new List<long>
            {
                0
            };
            var values = new List<Bitmap>
            {
                new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Maps.Data.Tests.TestImage.png"))
            };
            parser.SetReplaceCommandParameters(command, keys, values, 1);
            command.ExecuteNonQuery();
        }
    }
}