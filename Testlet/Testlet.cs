﻿using System;
using System.Collections.Generic;

namespace Testlet
{
    public class Testlet
    {
        public string TestletId;
        private readonly List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            if (Items?.Count != 10)
            {
                throw new ArgumentException();
            }

            return new List<Item>();
        }
    }
}
