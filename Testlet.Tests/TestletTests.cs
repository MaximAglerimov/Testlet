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
        [TestMethod]
        public void Randomize_EmptyItemsListPassedToConstructor_ReturnsEmptyList()
        {
            var expected = Enumerable.Empty<Item>().ToList();
            var actual = new Testlet(testletId: It.IsAny<string>(), items: It.IsAny<List<Item>>()).Randomize();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Randomize_IncompleteItemsListPassedToConstructor_ReturnsError()
        {
            var items = Enumerable.Repeat(new Item(), 8).ToList();
            var testlet = new Testlet(testletId: It.IsAny<string>(), items);

            Assert.ThrowsException<ArgumentException>(() => testlet.Randomize());
        }
    }
}
