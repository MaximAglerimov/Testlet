using System;

namespace Testlet
{
    public class Randomizer
    {
        private readonly Random _rnd;

        public Randomizer(int? seed = null)
        {
            _rnd = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        internal int Next()
        {
            return _rnd.Next();
        }
    }
}
