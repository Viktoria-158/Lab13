using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsLibraryVer2;

namespace TestsLAB13
{
    [TestClass]
    public class CollectionHandlerEventArgsTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string name = "TestCollection";
            string change = "add";
            object obj = new object();

            // Act
            var args = new CollectionHandlerEventArgs(name, change, obj);

            // Assert
            Assert.AreEqual(name, args.NameCollection);
            Assert.AreEqual(change, args.ChangeCollection);
            Assert.AreEqual(obj, args.Obj);
        }

        [TestMethod]
        public void Constructor_WithNullValues_ShouldInitializeProperties()
        {
            // Arrange
            string name = null;
            string change = null;
            object obj = null;

            // Act
            var args = new CollectionHandlerEventArgs(name, change, obj);

            // Assert
            Assert.IsNull(args.NameCollection);
            Assert.IsNull(args.ChangeCollection);
            Assert.IsNull(args.Obj);
        }
    }
}