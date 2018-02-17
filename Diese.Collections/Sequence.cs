using System;
using System.Collections.Generic;

namespace Diese.Collections
{
    static public class Sequence
    {
        static public IEnumerable<T> Aggregate<T>(T seed, Func<T, T> nextSelector)
        {
            for (T current = seed; current != null; current = nextSelector(current))
                yield return current;
        }

        static public IEnumerable<T> AggregateExclusive<T, TBase>(TBase ignoredSeed, Func<TBase, T> nextSelector)
            where T : TBase
        {
            for (T current = nextSelector(ignoredSeed); current != null; current = nextSelector(current))
                yield return current;
        }
    }
}