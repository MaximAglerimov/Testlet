using System;
using System.Collections.Generic;
using System.Linq;

namespace Testlet
{
    public class Testlet
    {
        private const int ItemsCount = 10;

        public string TestletId;
        private readonly List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            if (Items?.Count != ItemsCount)
            {
                throw new ArgumentException();
            }

            return new List<Item> {
                new Item { ItemType = ItemTypeEnum.Pretest },
                new Item { ItemType = ItemTypeEnum.Pretest }
            }.Concat(Enumerable.Repeat(new Item(), 8)).ToList();
        }
    }
}
