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

        public Testlet(string testletId, List<Item> items, Randomizer randomizer)
        {
            TestletId = testletId;
            Items = items;

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

            var rnd = new Random();
            var resultItems = new List<Item>();

            var randomizedPretestItems = pretestItems.OrderBy(item => rnd.Next()).ToList();
            var randomizedRestItems = randomizedPretestItems.Skip(FirstPretestItemsCount).Concat(operationalItems).OrderBy(item => rnd.Next()).ToList();

            resultItems.AddRange(randomizedPretestItems.Take(FirstPretestItemsCount));
            resultItems.AddRange(randomizedRestItems);

            return resultItems;
        }
    }
}
