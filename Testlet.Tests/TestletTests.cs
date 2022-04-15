using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Testlet.Tests
{
    [TestClass]
    public class TestletTests
    {
        private const int PretestItemsCount = 4;
        private const int OperationalItemsCount = 6;
        private const int FirstPretestItemsCount = 2;

        private Mock<Randomizer> randomizer;

        [TestInitialize]
        public void Setup() => randomizer = new Mock<Randomizer>(null);

        [TestMethod]
        public void Randomize_IncompleteItemsListPassedToConstructor_ReturnsError()
        {
            var items = GetItems(pretestItemsCount: 8, operationalItemsCount: 0);

            Assert.ThrowsException<ArgumentException>(() => new Testlet(testletId: It.IsAny<string>(), items, randomizer.Object));
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructorContainsInvalidOperationalItemsCount_ReturnsError()
        {
            var items = GetItems(pretestItemsCount: 3, operationalItemsCount: 7);

            Assert.ThrowsException<ArgumentException>(() => new Testlet(testletId: It.IsAny<string>(), items, randomizer.Object));
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_Returns10Items()
        {
            var items = GetItems(PretestItemsCount, OperationalItemsCount);

            var actual = new Testlet(testletId: It.IsAny<string>(), items, randomizer.Object).Randomize();

            Assert.AreEqual(10, actual.Count);
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_ReturnsFirst2ItemsArePretest()
        {
            var items = GetItems(PretestItemsCount, OperationalItemsCount);

            var actual = new Testlet(testletId: It.IsAny<string>(), items, randomizer.Object).Randomize();

            Assert.IsTrue(actual.Take(FirstPretestItemsCount).All(item => item.ItemType == ItemTypeEnum.Pretest));
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_ReturnsLast8ItemsContains6Operational()
        {
            var items = GetItems(PretestItemsCount, OperationalItemsCount);

            var actual = new Testlet(testletId: It.IsAny<string>(), items, randomizer.Object).Randomize();

            Assert.AreEqual(OperationalItemsCount, actual.Skip(FirstPretestItemsCount).Count(item => item.ItemType == ItemTypeEnum.Operational));
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_ReturnsRepeatedlyRandomizedData()
        {
            const int seed = 12345;
            const int iterationsCount = 10;

            var items = GetItems(PretestItemsCount, OperationalItemsCount);
            var randomizerMock = new Mock<Randomizer>(seed);
            var resultSample = new List<List<Item>>();

            for (var i = 0; i < iterationsCount; i++)
            {
                var testlet = new Testlet(testletId: It.IsAny<string>(), items, randomizerMock.Object);
                resultSample.Add(testlet.Randomize());
            }

            var areAllItemsEqual = false;
            for (var i = 0; i < iterationsCount - 1; i++)
            {
                areAllItemsEqual = resultSample[i].SequenceEqual(resultSample[i + 1]);

                if (!areAllItemsEqual)
                {
                    break;
                }
            }

            Assert.IsTrue(areAllItemsEqual);
        }

        private static List<Item> GetItems(int pretestItemsCount, int operationalItemsCount)
        {
            var pretestItems = Enumerable.Repeat(new Item(), pretestItemsCount).ToList();
            var operationalItems = Enumerable.Repeat(new Item() { ItemType = ItemTypeEnum.Operational }, operationalItemsCount).ToList();

            return pretestItems.Concat(operationalItems).ToList();
        }
    }
}
