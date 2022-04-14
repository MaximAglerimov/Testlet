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

            if (Items.Where(item => item.ItemType == ItemTypeEnum.Operational).Count() != 6)
            {
                throw new ArgumentException();
            }

            return new List<Item> {
                new Item { ItemType = ItemTypeEnum.Pretest },
                new Item { ItemType = ItemTypeEnum.Pretest },
                new Item { ItemType = ItemTypeEnum.Pretest },
                new Item { ItemType = ItemTypeEnum.Pretest },
                new Item { ItemType = ItemTypeEnum.Operational },
                new Item { ItemType = ItemTypeEnum.Operational },
                new Item { ItemType = ItemTypeEnum.Operational },
                new Item { ItemType = ItemTypeEnum.Operational },
                new Item { ItemType = ItemTypeEnum.Operational },
                new Item { ItemType = ItemTypeEnum.Operational },
            };
        }
    }
}
