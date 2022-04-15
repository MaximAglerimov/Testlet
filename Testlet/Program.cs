using System;
using System.Collections.Generic;

namespace Testlet
{
    internal static class Program
    {
        private static void Main()
        {
            var items = new List<Item>
            {
                new Item { ItemId = "1", ItemType = ItemTypeEnum.Pretest },
                new Item { ItemId = "2", ItemType = ItemTypeEnum.Pretest },
                new Item { ItemId = "3", ItemType = ItemTypeEnum.Pretest },
                new Item { ItemId = "4", ItemType = ItemTypeEnum.Pretest },
                new Item { ItemId = "5", ItemType = ItemTypeEnum.Operational },
                new Item { ItemId = "6", ItemType = ItemTypeEnum.Operational },
                new Item { ItemId = "7", ItemType = ItemTypeEnum.Operational },
                new Item { ItemId = "8", ItemType = ItemTypeEnum.Operational },
                new Item { ItemId = "9", ItemType = ItemTypeEnum.Operational },
                new Item { ItemId = "10", ItemType = ItemTypeEnum.Operational }
            };

            var randomizedItems = new Testlet("1", items).Randomize();

            foreach (var item in randomizedItems)
            {
                Console.WriteLine($"ItemId: {item.ItemId}, ItemType: {item.ItemType}");
            }
        }
    }
}
