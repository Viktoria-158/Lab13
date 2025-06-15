using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsLibraryVer2;

namespace TestsLAB13
{
    [TestClass]
    public class JournalEntryTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string name = "TestCollection";
            string change = "add";
            string data = "TestData";

            // Act
            var entry = new JournalEntry(name, change, data);

            // Assert
            Assert.AreEqual(name, entry.NameCollection);
            Assert.AreEqual(change, entry.ChangeType);
            Assert.AreEqual(data, entry.ObjectData);
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var entry = new JournalEntry("Test", "remove", "Item1");

            // Act
            var result = entry.ToString();

            // Assert
            StringAssert.Contains(result, "Коллекция: Test");
            StringAssert.Contains(result, "Изменение: remove");
            StringAssert.Contains(result, "Данные: Item1");
        }

        [TestMethod]
        public void Constructor_WithNullValues_ShouldInitializeProperties()
        {
            // Arrange
            string name = null;
            string change = null;
            string data = null;

            // Act
            var entry = new JournalEntry(name, change, data);

            // Assert
            Assert.IsNull(entry.NameCollection);
            Assert.IsNull(entry.ChangeType);
            Assert.IsNull(entry.ObjectData);
        }
    }
}