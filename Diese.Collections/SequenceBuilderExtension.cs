using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    static public class SequenceBuilderExtension
    {
        static public IEnumerable<T> ThenAdd<T>(this IEnumerable<T> sequence, params T[] nextValues)
        {
            return Enumerable.Concat(sequence, nextValues);
        }

        static public IEnumerable<T> ThenAdd<T>(this IEnumerable<T> sequence, IEnumerable<T> nextValues)
        {
            return Enumerable.Concat(sequence, nextValues);
        }

        static public IEnumerable<T> ThenStack<T>(this IEnumerable<T> sequence, params T[] nextValues)
        {
            return Enumerable.Concat(nextValues, sequence);
        }

        static public IEnumerable<T> ThenStack<T>(this IEnumerable<T> sequence, IEnumerable<T> nextValues)
        {
            return Enumerable.Concat(nextValues, sequence);
        }

        static public IEnumerable<T> ThenInsertAt<T>(this IEnumerable<T> sequence, int index, params T[] nextValues)
        {
            int i = 0;
            using (IEnumerator<T> enumerator = sequence.GetEnumerator())
            {
                while (i < index && enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    i++;
                }

                foreach (T nextValue in nextValues)
                    yield return nextValue;

                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    i++;
                }
            }
        }

        static public IEnumerable<T> ThenInsertAt<T>(this IEnumerable<T> sequence, int index, IEnumerable<T> nextValues)
        {
            int i = 0;
            using (IEnumerator<T> enumerator = sequence.GetEnumerator())
            {
                while (i < index && enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    i++;
                }

                foreach (T nextValue in nextValues)
                    yield return nextValue;

                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    i++;
                }
            }
        }

        static public IEnumerable<T> ThenGoTo<T>(this IEnumerable<T> sequence, T goal, Func<T, T> incrementor)
        {
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();
            foreach (T value in enumerable)
                yield return value;

            for (T value = incrementor(enumerable.Last()); !value.Equals(goal); value = incrementor(value))
                yield return value;

            yield return goal;
        }

        static public IEnumerable<T> ThenRepeat<T>(this IEnumerable<T> sequence, int times, IEnumerable<T> subsequence)
        {
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();
            foreach (T value in enumerable)
                yield return value;

            IEnumerable<T> values = subsequence as T[] ?? subsequence.ToArray();
            for (int t = 0; t < times; t++)
                foreach (T value in values)
                    yield return value;
        }

        static public IEnumerable<T> ThenRepeatCurrent<T>(this IEnumerable<T> sequence, int times = 1)
        {
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();
            foreach (T value in enumerable)
                yield return value;

            for (int t = 0; t < times; t++)
                foreach (T value in enumerable)
                    yield return value;
        }

        static public IEnumerable<T> ThenReverse<T>(this IEnumerable<T> sequence, bool copyFirst = false)
        {
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();
            foreach (T value in enumerable)
                yield return value;

            foreach (T value in enumerable.Reverse().Skip(copyFirst ? 0 : 1))
                yield return value;
        }

        static public IEnumerable<T> ThenLoop<T>(this IEnumerable<T> sequence)
        {
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();
            foreach (T value in enumerable)
                yield return value;

            foreach (T value in enumerable.Skip(1).Reverse().Skip(1))
                yield return value;
        }

        static public IEnumerable<T> ShiftOf<T>(this IEnumerable<T> sequence, int startIndex)
        {
            IEnumerable<T> enumerable = sequence as T[] ?? sequence.ToArray();

            foreach (T value in enumerable.Skip(startIndex))
                yield return value;

            foreach (T value in enumerable.Take(startIndex))
                yield return value;
        }
    }
}
