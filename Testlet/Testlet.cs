using System;
using System.Collections.Generic;
using System.Linq;

namespace Testlet
{
    public class Testlet
    {
        private const int ItemsCount = 10;
        private const int OperationalItemsCount = 6;
        private const int FirstPretestItemsCount = 2;

        public string TestletId;
        private readonly List<Item> Items;
        private readonly Randomizer Randomizer;

        public Testlet(string testletId, List<Item> items, Randomizer randomizer)
        {
            TestletId = testletId;
            Items = items;
            Randomizer = randomizer;

            if (Items?.Count != ItemsCount ||
                Items.Where(item => item.ItemType == ItemTypeEnum.Operational).Count() != OperationalItemsCount)
            {
                throw new ArgumentException();
            }
        }

        public List<Item> Randomize()
        {
            var pretestItems = Items.Where(item => item.ItemType == ItemTypeEnum.Pretest);
            var operationalItems = Items.Where(item => item.ItemType == ItemTypeEnum.Operational);

            var resultItems = new List<Item>();

            var randomizedPretestItems = pretestItems.OrderBy(item => Randomizer.Next()).ToList();
            var randomizedRestItems = randomizedPretestItems.Skip(FirstPretestItemsCount).Concat(operationalItems).OrderBy(item => Randomizer.Next()).ToList();

            resultItems.AddRange(randomizedPretestItems.Take(FirstPretestItemsCount));
            resultItems.AddRange(randomizedRestItems);

            return resultItems;
        }
    }
}
