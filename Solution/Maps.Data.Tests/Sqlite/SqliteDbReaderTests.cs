using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Maps.Data.Sqlite;
using Mono.Data.Sqlite;
using NUnit.Framework;

namespace Maps.Data.Tests.Sqlite
{
    [TestFixture]
    public class SqliteDbReaderTests
    {
        [DbStoreContract]
        private class TestDbStorable
        {
            [DbStoreMember(0, PrimaryKey = true, Name = "test_column_name")]
            public long Int64TestMember;

            [DbStoreMember(1)]
            public long? NullableInt64TestMember;

            [DbStoreMember(2, Name = "")]
            public bool BooleanTestMember;

            [DbStoreMember(3)]
            public bool? NullableBooleanTestMember;

            [DbStoreMember(4, Name = null)]
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
                var reader = new SqliteDbReader<long, TestDbStorable>(conn, parser);
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
                var reader = new SqliteDbReader<long, string>(conn, parser);
            });
        }

        [Test]
        public void TestReadValueMethodDbStoreContract()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, TestDbStorable>(conn, parser);

            var a = new TestDbStorable(12)
            {
                BooleanTestMember = true,
                NullableBooleanTestMember = null,
                NullableInt64TestMember = 14,
                StringTestMember = "test_string"
            };

            Assert.DoesNotThrow(() =>
            {
                writer.Write(12, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, TestDbStorable>(conn, parser);

            TestDbStorable b = null;
            Assert.DoesNotThrow(() =>
            {
                b = reader.Read(12);
            });

            Assert.IsNotNull(b);

            Assert.AreEqual(a.Int64TestMember, b.Int64TestMember);
            Assert.AreEqual(a.BooleanTestMember, b.BooleanTestMember);
            Assert.AreEqual(a.NullableInt64TestMember, b.NullableInt64TestMember);
            Assert.AreEqual(a.NullableBooleanTestMember, b.NullableBooleanTestMember);
            Assert.AreEqual(a.StringTestMember, b.StringTestMember);
        }

        [Test]
        public void TestReadValueMethodDbStoreContractList()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, TestDbStorable>(conn, parser);

            var a = new TestDbStorable(12)
            {
                BooleanTestMember = true,
                NullableBooleanTestMember = null,
                NullableInt64TestMember = 14,
                StringTestMember = "test_string"
            };

            var b = new TestDbStorable(67)
            {
                BooleanTestMember = false,
                NullableBooleanTestMember = true,
                NullableInt64TestMember = 192,
                StringTestMember = "test_string0"
            };

            Assert.DoesNotThrow(() =>
            {
                writer.Write(a.Int64TestMember, a);
                writer.Write(b.Int64TestMember, b);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, TestDbStorable>(conn, parser);

            IList<TestDbStorable> c = null;
            Assert.DoesNotThrow(() =>
            {
                c = reader.Read(new List<long>
                {
                    12,
                    67
                });
            });

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);

            Assert.IsNotNull(c[0]);
            Assert.AreEqual(c[0].Int64TestMember, a.Int64TestMember);
            Assert.AreEqual(c[0].BooleanTestMember, a.BooleanTestMember);
            Assert.AreEqual(c[0].NullableInt64TestMember, a.NullableInt64TestMember);
            Assert.AreEqual(c[0].NullableBooleanTestMember, a.NullableBooleanTestMember);
            Assert.AreEqual(c[0].StringTestMember, a.StringTestMember);

            Assert.IsNotNull(c[1]);
            Assert.AreEqual(c[1].Int64TestMember, b.Int64TestMember);
            Assert.AreEqual(c[1].BooleanTestMember, b.BooleanTestMember);
            Assert.AreEqual(c[1].NullableInt64TestMember, b.NullableInt64TestMember);
            Assert.AreEqual(c[1].NullableBooleanTestMember, b.NullableBooleanTestMember);
            Assert.AreEqual(c[1].StringTestMember, b.StringTestMember);
        }

        [Test]
        public void TestReadValueMethodString()
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
                writer.Write(12, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, string>(conn, parser);

            string b = null;
            Assert.DoesNotThrow(() =>
            {
                b = reader.Read(12);
            });

            Assert.IsNotNull(b);
            Assert.AreEqual(a, b);
        }

        [Test]
        public void TestReadValueMethodStringList()
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
            var b = "test_string0";

            Assert.DoesNotThrow(() =>
            {
                writer.Write(12, a);
                writer.Write(67, b);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, string>(conn, parser);

            IList<string> c = null;
            Assert.DoesNotThrow(() =>
            {
                c = reader.Read(new List<long>
                {
                    12,
                    67
                });
            });

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);

            Assert.IsNotNull(c[0]);
            Assert.AreEqual(a, c[0]);
            Assert.IsNotNull(c[1]);
            Assert.AreEqual(b, c[1]);
        }

        [Test]
        public void TestReadValueMethodBitmap()
        {
            var parser = new SqliteDbTypeParser<long, Bitmap>("image_table");
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
                writer.Write(12, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, Bitmap>(conn, parser);

            Bitmap b = null;
            Assert.DoesNotThrow(() =>
            {
                b = reader.Read(12);
            });

            Assert.IsNotNull(b);
            
            // compare the hashes
            var sha512 = new SHA512CryptoServiceProvider();

            byte[] aHash;
            byte[] bHash;

            using (var stream = new MemoryStream())
            {
                a.Save(stream, ImageFormat.Png);
                aHash = sha512.ComputeHash(stream);
            }

            using (var stream = new MemoryStream())
            {
                b.Save(stream, ImageFormat.Png);
                bHash = sha512.ComputeHash(stream);
            }

            Assert.AreEqual(aHash, bHash);
        }

        [Test]
        public void TestReadValueMethodBitmapList()
        {
            var parser = new SqliteDbTypeParser<long, Bitmap>("image_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, Bitmap>(conn, parser);

            var a = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Maps.Data.Tests.TestImage.png"));
            var b = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("Maps.Data.Tests.TestImage0.png"));

            Assert.DoesNotThrow(() =>
            {
                writer.Write(12, a);
                writer.Write(67, b);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, Bitmap>(conn, parser);

            IList<Bitmap> c = null;
            Assert.DoesNotThrow(() =>
            {
                c = reader.Read(new List<long>
                {
                    12,
                    67
                });
            });

            Assert.IsNotNull(c);
            Assert.AreEqual(2, c.Count);

            Assert.IsNotNull(c[0]);
            Assert.IsNotNull(c[1]);

            // compare the hashes
            var md5 = new MD5CryptoServiceProvider();

            byte[] aHash;

            using (var stream = new MemoryStream())
            {
                a.Save(stream, ImageFormat.Png);
                aHash = md5.ComputeHash(stream);
            }

            byte[] bHash;

            using (var stream = new MemoryStream())
            {
                b.Save(stream, ImageFormat.Png);
                bHash = md5.ComputeHash(stream);
            }

            byte[] cHash;

            using (var stream = new MemoryStream())
            {
                c[0].Save(stream, ImageFormat.Png);
                cHash = md5.ComputeHash(stream);
            }

            Assert.AreEqual(aHash, cHash);

            using (var stream = new MemoryStream())
            {
                c[1].Save(stream, ImageFormat.Png);
                cHash = md5.ComputeHash(stream);
            }

            Assert.AreEqual(bHash, cHash);
        }

        [Test]
        public void TestReadKeyMethodDbStoreContract()
        {
            var parser = new SqliteDbTypeParser<long, TestDbStorable>("db_storable_table");
            parser.Parse();

            var connectionString = SqliteDbConnection.ConnectionString(true, false);
            var conn = new SqliteConnection(connectionString);
            conn.Open();

            var createTableCommand = new SqliteCommand(parser.CreateTable, conn);
            createTableCommand.ExecuteNonQuery();

            var writer = new SqliteDbWriter<long, TestDbStorable>(conn, parser);

            var a = new TestDbStorable(12)
            {
                BooleanTestMember = true,
                NullableBooleanTestMember = null,
                NullableInt64TestMember = 14,
                StringTestMember = "test_string"
            };

            Assert.DoesNotThrow(() =>
            {
                writer.Write(12, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, TestDbStorable>(conn, parser);

            long aKey = 0;
            Assert.DoesNotThrow(() =>
            {
                aKey = reader.Read(a);
            });

            Assert.AreEqual(a.Int64TestMember, aKey);
        }

        [Test]
        public void TestReadKeyMethodString()
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
                writer.Write(12, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, string>(conn, parser);

            long aKey = 0;
            Assert.DoesNotThrow(() =>
            {
                aKey = reader.Read(a);
            });

            Assert.AreEqual(12, aKey);
        }

        [Test]
        public void TestReadKeyMethodBitmap()
        {
            var parser = new SqliteDbTypeParser<long, Bitmap>("image_table");
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
                writer.Write(12, a);
            });

            Assert.DoesNotThrow(() =>
            {
                writer.Flush();
            });

            var reader = new SqliteDbReader<long, Bitmap>(conn, parser);

            long aKey = 0;
            Assert.DoesNotThrow(() =>
            {
                aKey = reader.Read(a);
            });

            Assert.AreEqual(12, aKey);
        }
    }
}