using System.Linq;
using Hdd.EfData;
using NUnit.Framework;

namespace EfData.Tests
{
    [TestFixture]
    public class DatabaseManagerTests
    {
        private const string TestDatabasePath = "test.db";

        [SetUp]
        public void SetUp()
        {
            var databaseManager = new DatabaseManager();

            databaseManager.DeleteDatabase(TestDatabasePath);
            databaseManager.CreateDatabase(TestDatabasePath);

            using var context = new DatabaseContext(TestDatabasePath);
            context.Parts.Add(TestPart1);
            context.Parts.Add(TestPart2);
            context.Parts.Add(TestPart3);
            context.Parts.Add(TestPart4);
            context.Parts.Add(TestPart5);

            context.SaveChanges();
        }

        private static readonly Part TestPart1 = new Part {Id = 1, Name = "1000"};
        private static readonly Part TestPart2 = new Part {Id = 2, Name = "2000"};
        private static readonly Part TestPart3 = new Part {Id = 3, Name = "3000"};
        private static readonly Part TestPart4 = new Part {Id = 4, Name = "4000"};
        private static readonly Part TestPart5 = new Part {Id = 5, Name = "5000"};

        [Test]
        public void DatabaseManager_Context_AddTestData_ContainsPartIds()
        {
            using var context = new DatabaseContext(TestDatabasePath);

            var parts = context.Parts.ToList();

            Assert.AreEqual(1, parts[0].Id);
            Assert.AreEqual(2, parts[1].Id);
            Assert.AreEqual(3, parts[2].Id);
            Assert.AreEqual(4, parts[3].Id);
            Assert.AreEqual(5, parts[4].Id);
        }

        [Test]
        public void DatabaseManager_Context_AddTestData_ContainsPartNames()
        {
            using var context = new DatabaseContext(TestDatabasePath);

            var parts = context.Parts.ToList();

            Assert.AreEqual("1000", parts[0].Name);
            Assert.AreEqual("2000", parts[1].Name);
            Assert.AreEqual("3000", parts[2].Name);
            Assert.AreEqual("4000", parts[3].Name);
            Assert.AreEqual("5000", parts[4].Name);
        }
    }
}
