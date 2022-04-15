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

        [TestMethod]
        public void Randomize_IncompleteItemsListPassedToConstructor_ReturnsError()
        {
            var items = GetItems(pretestItemsCount: 8, operationalItemsCount: 0);

            Assert.ThrowsException<ArgumentException>(() => new Testlet(testletId: It.IsAny<string>(), items));
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructorContainsInvalidOperationalItemsCount_ReturnsError()
        {
            var items = GetItems(pretestItemsCount: 3, operationalItemsCount: 7);

            Assert.ThrowsException<ArgumentException>(() => new Testlet(testletId: It.IsAny<string>(), items));
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_Returns10Items()
        {
            var items = GetItems(PretestItemsCount, OperationalItemsCount);

            var actual = new Testlet(testletId: It.IsAny<string>(), items).Randomize();

            Assert.AreEqual(10, actual.Count);
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_ReturnsFirst2ItemsArePretest()
        {
            var items = GetItems(PretestItemsCount, OperationalItemsCount);

            var actual = new Testlet(testletId: It.IsAny<string>(), items).Randomize();

            Assert.IsTrue(actual.Take(FirstPretestItemsCount).All(item => item.ItemType == ItemTypeEnum.Pretest));
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_ReturnsLast8ItemsContains6Operational()
        {
            var items = GetItems(PretestItemsCount, OperationalItemsCount);

            var actual = new Testlet(testletId: It.IsAny<string>(), items).Randomize();

            Assert.AreEqual(OperationalItemsCount, actual.Skip(FirstPretestItemsCount).Count(item => item.ItemType == ItemTypeEnum.Operational));
        }

        private static List<Item> GetItems(int pretestItemsCount, int operationalItemsCount)
        {
            var pretestItems = Enumerable.Repeat(new Item(), pretestItemsCount).ToList();
            var operationalItems = Enumerable.Repeat(new Item() { ItemType = ItemTypeEnum.Operational }, operationalItemsCount).ToList();

            return pretestItems.Concat(operationalItems).ToList();
        }
    }
}
