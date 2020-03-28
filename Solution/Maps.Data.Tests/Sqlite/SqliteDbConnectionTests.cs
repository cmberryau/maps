using Maps.Data.Sqlite;
using NUnit.Framework;

namespace Maps.Data.Tests.Sqlite
{
    [TestFixture]
    public class SqliteDbConnectionTests
    {
        [Test]
        public void TestConstructor()
        {
            Assert.DoesNotThrow(() =>
            {
                new SqliteDbConnection<long, string>(string.Empty, "string_table");
            });
        }

        [Test]
        public void TestClearMethod()
        {
            var conn = new SqliteDbConnection<long, string>(SqliteTestUtilities.TempTestDbFullPath, "string_table");
            conn.Clear();
        }

        [Test]
        public void TestMetaWriterReaderMethods()
        {
            var conn = new SqliteDbConnection<long, string>(SqliteTestUtilities.TempTestDbFullPath, "string_table");
            conn.Clear();

            var metaBytes = new[]
            {
                (byte) 1, (byte) 2, (byte) 3, (byte) 4
            };

            using (var writer = conn.MetaWriter())
            {
                writer.Write("hello", metaBytes);
            }

            using (var reader = conn.MetaReader())
            {
                var bytes = reader.Read("hello");

                Assert.IsNotNull(bytes);
                Assert.AreEqual(metaBytes.Length, bytes.Length);
                for (var i = 0; i < bytes.Length; i++)
                {
                    Assert.AreEqual(metaBytes[i], bytes[i]);
                }
            }
        }

        [Test]
        public void TestCountProperty()
        {
            var conn = new SqliteDbConnection<long, string>(SqliteTestUtilities.TempTestDbFullPath, "string_table");
            conn.Clear();

            Assert.AreEqual(0, conn.Count);

            using (var writer = conn.Writer())
            {
                writer.Write(0, "test_string0");
            }

            Assert.AreEqual(1, conn.Count);

            using (var writer = conn.Writer())
            {
                writer.Write(1, "test_string1");
            }

            Assert.AreEqual(2, conn.Count);
        }
    }
}