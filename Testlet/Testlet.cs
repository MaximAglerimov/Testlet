using System;
using System.Collections.Generic;
using System.Linq;

namespace Testlet
{
    public class Testlet
    {
        private const int ItemsCount = 10;
        private const int OperationalItemsCount = 6;

        public string TestletId;
        private readonly List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            if (Items?.Count != ItemsCount ||
                Items.Where(item => item.ItemType == ItemTypeEnum.Operational).Count() != OperationalItemsCount)
            {
                throw new ArgumentException();
            }

            var pretestItems = Items.Where(item => item.ItemType == ItemTypeEnum.Pretest);
            var operationalItems = Items.Where(item => item.ItemType == ItemTypeEnum.Operational);

            var rnd = new Random();
            var resultItems = new List<Item>();

            var randomizedPretestItems = pretestItems.OrderBy(item => rnd.Next()).ToList();
            var rest8Items = randomizedPretestItems.Skip(2).Concat(operationalItems).OrderBy(item => rnd.Next()).ToList();

            resultItems.AddRange(randomizedPretestItems.Take(2));
            resultItems.AddRange(rest8Items);

            return resultItems;
        }
    }
}
