using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsLibraryVer2;
using System;

namespace TestsLAB13
{
    [TestClass]
    public class MyObservableCollectionTests
    {
        private bool eventRaised;
        private CollectionHandlerEventArgs eventArgs;

        private void CollectionEventHandler(object source, CollectionHandlerEventArgs args)
        {
            eventRaised = true;
            eventArgs = args;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            eventRaised = false;
            eventArgs = null;
        }

        [TestMethod]
        public void Add_ShouldRaiseCollectionCountChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<Plant>("Test");
            collection.CollectionCountChanged += CollectionEventHandler;

            // Act
            collection.Add(new Plant());

            // Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void Remove_ShouldRaiseCollectionCountChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<Plant>("Test");
            var plant = new Plant();
            collection.Add(plant);
            collection.CollectionCountChanged += CollectionEventHandler;

            // Act
            collection.Remove(plant);

            // Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void IndexerSet_ShouldRaiseCollectionReferenceChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<Plant>("Test");
            collection.Add(new Plant());
            collection.CollectionReferenceChanged += CollectionEventHandler;

            // Act
            collection[0] = new Plant();

            // Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void RemoveAt_ShouldRaiseCollectionCountChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<Plant>("Test");
            collection.Add(new Plant());
            collection.CollectionCountChanged += CollectionEventHandler;

            // Act
            collection.RemoveAt(0);

            // Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerSet_WithInvalidIndex_ShouldThrow()
        {
            // Arrange
            var collection = new MyObservableCollection<Plant>("Test");

            // Act
            collection[0] = new Plant();

            // Assert - ожидается исключение
        }

        [TestMethod]
        public void Events_ShouldPassCorrectArguments()
        {
            // Arrange
            var collection = new MyObservableCollection<Plant>("TestCollection");
            collection.CollectionCountChanged += CollectionEventHandler;
            var plant = new Plant();

            // Act
            collection.Add(plant);

            // Assert
            Assert.AreEqual("TestCollection", eventArgs.NameCollection);
            Assert.AreEqual("add", eventArgs.ChangeCollection);
            Assert.AreEqual(plant, eventArgs.Obj);
        }

        [TestMethod]
        public void Constructor_WithName_ShouldSetNameProperty()
        {
            // Arrange & Act
            var collection = new MyObservableCollection<Plant>("TestName");

            // Assert
            Assert.AreEqual("TestName", collection.Name);
        }
    }
}