using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsLibraryVer2;
using System.Linq;

namespace TestsLAB13
{
    [TestClass]
    public class JournalTests
    {
        [TestMethod]
        public void Add_ShouldAddEntryToJournal()
        {
            // Arrange
            var journal = new Journal();
            var entry = new JournalEntry("Test", "add", "Item1");

            // Act
            journal.Add(entry);

            // Assert
            Assert.AreEqual(1, journal.ToString().Split('\n').Count(s => !string.IsNullOrEmpty(s)));
        }

        [TestMethod]
        public void PrintJournal_WithEmptyJournal_ShouldNotThrow()
        {
            // Arrange
            var journal = new Journal();

            // Act & Assert
            try
            {
                journal.PrintJournal();
            }
            catch
            {
                Assert.Fail("Пустой журнал");
            }
        }

        [TestMethod]
        public void ToString_WithMultipleEntries_ShouldReturnAllEntries()
        {
            // Arrange
            var journal = new Journal();
            journal.Add(new JournalEntry("Test1", "add", "Item1"));
            journal.Add(new JournalEntry("Test2", "remove", "Item2"));

            // Act
            var result = journal.ToString();

            // Assert
            StringAssert.Contains(result, "Item1");
            StringAssert.Contains(result, "Item2");
            Assert.AreEqual(2, result.Split('\n').Count(s => s.Contains("Коллекция:")));
        }

        [TestMethod]
        public void Add_NullEntry_ShouldNotThrow()
        {
            // Arrange
            var journal = new Journal();

            // Act & Assert
            try
            {
                journal.Add(null);
            }
            catch
            {
                Assert.Fail("Нулевая запись");
            }
        }
    }
}