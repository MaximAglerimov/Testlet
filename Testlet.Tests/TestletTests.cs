using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Testlet.Tests
{
    [TestClass]
    public class TestletTests
    {
        [TestMethod]
        public void Randomize_IncompleteItemsListPassedToConstructor_ReturnsError()
        {
            var items = Enumerable.Repeat(new Item(), 8).ToList();
            var testlet = new Testlet(testletId: It.IsAny<string>(), items);

            Assert.ThrowsException<ArgumentException>(() => testlet.Randomize());
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_Returns10Items()
        {
            const int itemsCount = 10;

            var items = Enumerable.Repeat(new Item(), itemsCount).ToList();
            var actual = new Testlet(testletId: It.IsAny<string>(), items).Randomize();

            Assert.AreEqual(itemsCount, actual.Count);
        }

        [TestMethod]
        public void Randomize_ItemsListPassedToConstructor_ReturnsFirst2ItemsArePretest()
        {
            const int itemsCount = 10;

            var items = Enumerable.Repeat(new Item(), itemsCount).ToList();
            var actual = new Testlet(testletId: It.IsAny<string>(), items).Randomize();

            Assert.IsTrue(actual.Take(2).All(item => item.ItemType == ItemTypeEnum.Pretest));
        }
    }
}
