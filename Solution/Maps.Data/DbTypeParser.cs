using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Maps.Data
{
    /// <summary>
    /// Base class responsible for parsing database types
    /// </summary>
    /// <typeparam name="TKey">The default key type for the db table</typeparam>
    /// <typeparam name="TValue">The value type for the db table</typeparam>
    public abstract class DbTypeParser<TKey, TValue>
    {
        /// <summary>
        /// The default primary key idex
        /// </summary>
        protected int PrimaryKeyIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Was the primary key additionally generated?
        /// </summary>
        protected bool GeneratedPrimaryKey
        {
            get;
            private set;
        }

        /// <summary>
        /// The number of primary keys
        /// </summary>
        protected int PrimaryKeyCount
        {
            get;
            private set;
        }

        /// <summary>
        /// The columns marked as primary keys
        /// </summary>
        protected IList<bool> PrimaryKeys
        {
            get;
        }

        /// <summary>
        /// The columns marked as unique
        /// </summary>
        protected IList<bool> Unique
        {
            get;
        }

        /// <summary>
        /// The number of columns
        /// </summary>
        protected int ColumnCount
        {
            get;
            private set;
        }

        /// <summary>
        /// The list of column types
        /// </summary>
        protected IList<Type> Types
        {
            get;
        }

        /// <summary>
        /// The list of column names
        /// </summary>
        protected IList<string> Names
        {
            get;
        }

        /// <summary>
        /// The value type constructor
        /// </summary>
        protected Func<TValue> ValueConstructor
        {
            get;
            private set;
        }

        /// <summary>
        /// Is the value type a db store contract?
        /// </summary>
        protected bool DbStoreContract
        {
            get;
            private set;
        }

        /// <summary>
        /// The field info for value type when it's a db store contract
        /// </summary>
        protected IList<FieldInfo> Fields
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of DbTypeParser
        /// </summary>
        protected DbTypeParser()
        {
            PrimaryKeys = new List<bool>();
            Unique = new List<bool>();
            Types = new List<Type>();
            Names = new List<string>();
        }

        /// <summary>
        /// Initializes the DbTableOperator instance
        /// </summary>
        public virtual void Parse()
        {
            // validate that we can use the key type for a primary key
            var keyType = typeof(TKey);
            if (!ValidateTypeCanBePrimaryKey(keyType))
            {
                throw new InvalidOperationException($"Cannot use type {keyType} as primary key");
            }

            var valueType = typeof(TValue);
            // if the value type is a db store contract
            if (Attribute.GetCustomAttribute(valueType, typeof(DbStoreContract)) != null)
            {
                Fields = new List<FieldInfo>();
                var hasEmptyConstructor = false;

                // iterate through type members, detecting the DbStoreMembers
                var members = valueType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var member in members)
                {
                    // evaluate fields
                    if (member.MemberType == MemberTypes.Field)
                    {
                        // if the member is a db store member
                        if (Attribute.GetCustomAttribute(member, typeof(DbStoreMember)) is DbStoreMember dbStoreMember)
                        {
                            var memberType = ((FieldInfo)member).FieldType;
                            Fields.Add((FieldInfo)member);

                            // throw if we can't store the member type
                            if (!ValidateTypeIsStorable(memberType))
                            {
                                throw new InvalidOperationException($"Cannot store type {memberType}");
                            }

                            // increment the column count
                            ColumnCount++;

                            // mark has primary key if we find one
                            if (dbStoreMember.PrimaryKey)
                            {
                                PrimaryKeyCount++;
                            }

                            PrimaryKeys.Add(dbStoreMember.PrimaryKey);
                            Unique.Add(dbStoreMember.Unique);
                            Types.Add(memberType);

                            // add the member name, using default when null or empty
                            if (string.IsNullOrEmpty(dbStoreMember.Name))
                            {
                                Names.Add($"member_{dbStoreMember.Ordinal}");
                            }
                            else
                            {
                                Names.Add(dbStoreMember.Name);
                            }
                        }
                    }
                    // evaluate constructors
                    else if (member.MemberType == MemberTypes.Constructor)
                    {
                        var ctor = (ConstructorInfo)member;
                        var ctorParams = ctor.GetParameters();

                        if (ctorParams.Length == 0)
                        {
                            hasEmptyConstructor = true;
                            // set up the constructor func
                            ValueConstructor = () => (TValue)ctor.Invoke(null);
                        }
                    }
                }

                // validate that we have at least one primary key
                if (PrimaryKeyCount <= 0)
                {
                    throw new InvalidOperationException("DbStoreContract requires a primary key");
                }

                // validate that the type has an empty constructor
                if (!hasEmptyConstructor)
                {
                    throw new InvalidOperationException("DbStoreContract requires an empty constructor");
                }

                var foundDefaultKey = false;

                // if more than one primary key is provided
                if (PrimaryKeyCount > 1)
                {
                    if (keyType != typeof(long))
                    {
                        throw new NotSupportedException();
                    }

                    foundDefaultKey = true;
                    PrimaryKeyIndex = 0;

                    PrimaryKeys.Add(false);
                    Unique.Add(false);
                    Types.Add(null);
                    Names.Add(null);

                    // we need to shift all other columns along now
                    for (var i = ColumnCount; i > 0; --i)
                    {
                        PrimaryKeys[i] = PrimaryKeys[i - 1];
                        Unique[i] = Unique[i - 1];
                        Types[i] = Types[i - 1];
                        Names[i] = Names[i - 1];
                    }

                    PrimaryKeys[0] = true;
                    Unique[0] = true;
                    // generated primary key is always type long
                    Types[0] = typeof(long);
                    Names[0] = $"{valueType.Name.ToLower()}_gpk";
                    // mark that we generated an additional primary key
                    GeneratedPrimaryKey = true;
                    PrimaryKeyCount++;
                    ColumnCount++;
                }
                else
                {
                    // validate the requested primary keys
                    for (var i = 0; i < PrimaryKeyCount; ++i)
                    {
                        if (PrimaryKeys[i])
                        {
                            if (!ValidateTypeCanBePrimaryKey(Types[i]))
                            {
                                throw new InvalidOperationException($"Cannot use type {keyType} as primary key");
                            }

                            if (Types[i] == keyType)
                            {
                                foundDefaultKey = true;
                                PrimaryKeyIndex = i;
                            }
                        }
                    }
                }

                // validate that we have found the default key
                if (!foundDefaultKey)
                {
                    throw new InvalidOperationException($"Could not find default key for {valueType} in members");
                }

                // mark as a db store contract
                DbStoreContract = true;
            }
            // if the value type is not a db store contract
            else
            {
                // validate that we can store the value type
                if (!ValidateTypeIsStorable(valueType))
                {
                    throw new InvalidOperationException($"Cannot store type {valueType}");
                }

                // add the key type
                Types.Add(keyType);
                Names.Add("key");
                PrimaryKeys.Add(true);
                Unique.Add(true);

                // add the value type
                Types.Add(valueType);
                Names.Add("value");
                PrimaryKeys.Add(false);
                Unique.Add(false);

                // has two columns
                ColumnCount = 2;

                // set up the constructor
                ValueConstructor = () => (TValue)PrimitiveConstructor(valueType);
            }
        }

        /// <summary>
        /// Validates if the type is usable as a primary key on the db
        /// </summary>
        /// <param name="type">The type to test against</param>
        /// <returns>True if storable, false otherwise</returns>
        protected abstract bool ValidateTypeCanBePrimaryKey(Type type);

        /// <summary>
        /// Validates if the type is storable on the db
        /// </summary>
        /// <param name="type">The type to test against</param>
        /// <returns>True if storable, false otherwise</returns>
        protected abstract bool ValidateTypeIsStorable(Type type);

        private object PrimitiveConstructor(Type type)
        {
            if (type == typeof(long))
            {
                return new long();
            }
            if (type == typeof(long?))
            {
                return new long?();
            }
            if (type == typeof(bool))
            {
                return new bool();
            }
            if (type == typeof(bool?))
            {
                return new bool?();
            }
            if (type == typeof(string))
            {
                return string.Empty;
            }
            if (type == typeof(Bitmap))
            {
                return null;
            }

            throw new NotSupportedException();
        }
    }
}