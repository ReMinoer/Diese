using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Linseq
{
    static public class Sequence
    {
        static public IEnumerable<T> StartWith<T>(params T[] values)
        {
            return Enumerable.Empty<T>().ThenAdd(values);
        }

        static public IEnumerable<T> Recursive<T>(T start, Func<T, T> recursion, Predicate<T> doWhilePredicate)
        {
            T nextvalue = start;

            do
            {
                yield return nextvalue;
                nextvalue = recursion(nextvalue);
            } while (doWhilePredicate(nextvalue));
        }
    }
}