using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Maps.Data.Sqlite;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Maps.Data.Tests.Sqlite
{
    [TestFixture]
    public class SqliteDbWriterTests
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
        public void TestConstructorDbStoreContract()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            Assert.DoesNotThrow(() =>
            {
                var writer = new SqliteDbWriter<long, TestDbStorable>(conn, parser);
            });
        }

        [Test]
        public void TestConstructorString()
        {
            var parser = new SqliteDbTypeParser<long, string>("string_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            Assert.DoesNotThrow(() =>
            {
                var writer = new SqliteDbWriter<long, string>(conn, parser);
            });
        }

        [Test]
        public void TestWriteMethodDbStoreContract()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, TestDbStorable>(conn, parser);

            var a = new TestDbStorable(0);

            Assert.DoesNotThrow(() =>
            {
                writer.Write(0, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });
        }

        [Test]
        public void TestWriteMethodString()
        {
            var parser = new SqliteDbTypeParser<long, string>("string_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, string>(conn, parser);

            var a = "test_string";

            Assert.DoesNotThrow(() =>
            {
                writer.Write(0, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });
        }

        [Test]
        public void TestWriteMethodBitmap()
        {
            var parser = new SqliteDbTypeParser<long, Bitmap>("bitmap_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, Bitmap>(conn, parser);

            var a = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Maps.Data.Tests.TestImage.png"));

            Assert.DoesNotThrow(() =>
            {
                writer.Write(0, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });
        }

        [Test]
        public void TestWriteMethodListsDbStoreContract()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, TestDbStorable>(conn, parser);

            var keys = new List<long>
            {
                0,
                1
            };

            var values = new List<TestDbStorable>
            {
                new TestDbStorable(0),
                new TestDbStorable(1)
            };

            Assert.DoesNotThrow(() =>
            {
                writer.Write(keys, values);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });
        }

        [Test]
        public void TestWriteMethodListsString()
        {
            var parser = new SqliteDbTypeParser<long, string>("string_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, string>(conn, parser);

            var keys = new List<long>
            {
                0,
                1
            };

            var values = new List<string>
            {
                "test_string_0",
                "test_string_1",
            };

            Assert.DoesNotThrow(() =>
            {
                writer.Write(keys, values);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });
        }

        [Test]
        public void TestWriteMethodListsBitmap()
        {
            var parser = new SqliteDbTypeParser<long, Bitmap>("bitmap_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, Bitmap>(conn, parser);

            var keys = new List<long>
            {
                0,
                1
            };

            var values = new List<Bitmap>
            {
                new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Maps.Data.Tests.TestImage.png")),
                new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Maps.Data.Tests.TestImage0.png")),
            };

            Assert.DoesNotThrow(() =>
            {
                writer.Write(keys, values);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });
        }
    }
}