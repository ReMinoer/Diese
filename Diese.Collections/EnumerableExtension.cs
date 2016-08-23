using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    static public class EnumerableExtension
    {
        static public bool CountIsSuperiorTo<T>(this IEnumerable<T> enumerable, int number)
        {
            if (number <= 0)
                return true;

            int i = 0;
            IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                i++;
                if (i > number)
                    return true;
            }

            return false;
        }

        static public bool CountIsInferiorTo<T>(this IEnumerable<T> enumerable, int number)
        {
            if (number <= 0)
                return false;

            int i = 0;
            IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                i++;
                if (i >= number)
                    return false;
            }

            return true;
        }
        static public bool CountIsSuperiorOrEqualsTo<T>(this IEnumerable<T> enumerable, int number)
        {
            if (number < 0)
                return true;

            int i = 0;
            IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                i++;
                if (i >= number)
                    return true;
            }

            return false;
        }

        static public bool CountIsInferiorOrEqualsTo<T>(this IEnumerable<T> enumerable, int number)
        {
            if (number < 0)
                return false;

            int i = 0;
            IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                i++;
                if (i > number)
                    return false;
            }

            return true;
        }

        static public bool SetEquals<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn)
        {
            return enumerableOut.All(enumerableIn.Contains);
        }

        static public bool SetDiff<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, out IEnumerable<T> added, out IEnumerable<T> removed)
        {
            IEnumerable<T> arrayOut = enumerableOut as T[] ?? enumerableOut.ToArray();
            IEnumerable<T> arrayIn = enumerableIn as T[] ?? enumerableIn.ToArray();

            List<T> addedList = arrayIn.ToList();
            List<T> removedList = arrayOut.ToList();

            foreach (T itemOut in arrayOut)
                if (addedList.Contains(itemOut))
                    addedList.Remove(itemOut);


            foreach (T itemIn in arrayIn)
                if (removedList.Contains(itemIn))
                    removedList.Remove(itemIn);

            added = addedList;
            removed = removedList;

            return added.Any() || removed.Any();
        }

        static public bool SetDiff<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, out IEnumerable<T> added, out IEnumerable<T> removed, out IEnumerable<T> conserved)
        {
            IEnumerable<T> arrayOut = enumerableOut as T[] ?? enumerableOut.ToArray();
            IEnumerable<T> arrayIn = enumerableIn as T[] ?? enumerableIn.ToArray();

            List<T> addedList = arrayIn.ToList();
            List<T> removedList = arrayOut.ToList();
            var conservedList = new List<T>();

            foreach (T itemOut in arrayOut)
            {
                if (addedList.Contains(itemOut))
                {
                    addedList.Remove(itemOut);
                    conservedList.Add(itemOut);
                }
                else
                    removedList.Remove(itemOut);
            }

            foreach (T itemIn in arrayIn)
                if (removedList.Contains(itemIn))
                    removedList.Remove(itemIn);

            added = addedList;
            removed = removedList;
            conserved = conservedList;

            return added.Any() || removed.Any();
        }

        static public IEnumerable<TResult> JoinByOrdering<T, TResult>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Func<T, T, TResult> resultSelector, bool conserveOrder = true)
            where T : IComparable<T>
        {
            return JoinByOrdering(enumerableOut, enumerableIn, x => x, x => x, resultSelector, new ComparisonComparer<T>((x, y) => x.CompareTo(y)), conserveOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<T, TResult>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Func<T, T, TResult> resultSelector, Comparison<T> comparison, bool conserveOrder = true)
        {
            return JoinByOrdering(enumerableOut, enumerableIn, x => x, x => x, resultSelector, new ComparisonComparer<T>(comparison), conserveOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<T, TResult>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Func<T, T, TResult> resultSelector, IComparer<T> comparer, bool conserveOrder = true)
        {
            return JoinByOrdering(enumerableOut, enumerableIn, x => x, x => x, resultSelector, comparer, conserveOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, bool conserveOrder = true)
            where TKey : IComparable<TKey>
        {
            return JoinByOrdering(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, resultSelector, new ComparisonComparer<TKey>((x, y) => x.CompareTo(y)), conserveOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, Comparison<TKey> comparison, bool conserveOrder = true)
        {
            return JoinByOrdering(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, resultSelector, new ComparisonComparer<TKey>(comparison), conserveOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, IComparer<TKey> comparer, bool conserveOrder = true)
        {
            return conserveOrder
                ? enumerableOut.Select((key, index) => new KeyIndexPair<TOut>(key, index)).JoinByOrderingInternal(enumerableIn, x => keySelectorOut(x.Key), keySelectorIn, (x, y) => new KeyIndexPair<TResult>(resultSelector(x.Key, y), x.Index), comparer).OrderBy(x => x.Index).Select(x => x.Key)
                : JoinByOrderingInternal(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, resultSelector, comparer);
        }

        static private IEnumerable<TResult> JoinByOrderingInternal<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, IComparer<TKey> comparer)
        {
            TOut[] arrayOut = enumerableOut.ToArray();
            if (!arrayOut.Any())
                yield break;

            TIn[] arrayIn = enumerableIn.ToArray();
            if (!arrayIn.Any())
                yield break;

            IEnumerator<TOut> enumeratorOut = arrayOut.OrderBy(keySelectorOut, comparer).GetEnumerator();
            IEnumerator<TIn> enumeratorIn = arrayIn.OrderBy(keySelectorIn, comparer).GetEnumerator();

            enumeratorOut.MoveNext();
            enumeratorIn.MoveNext();
            TKey currentKeyOut = keySelectorOut(enumeratorOut.Current);
            TKey currentKeyIn = keySelectorIn(enumeratorIn.Current);

            while (true)
            {
                int comparison = comparer.Compare(currentKeyOut, currentKeyIn);

                if (comparison == 0)
                {
                    yield return resultSelector(enumeratorOut.Current, enumeratorIn.Current);

                    if (enumeratorIn.MoveNext())
                        yield break;
                    
                    if (enumeratorOut.MoveNext())
                        yield break;

                    currentKeyOut = keySelectorOut(enumeratorOut.Current);
                    currentKeyIn = keySelectorIn(enumeratorIn.Current);
                }
                else if (comparison < 0)
                {
                    if (enumeratorOut.MoveNext())
                        yield break;

                    currentKeyOut = keySelectorOut(enumeratorOut.Current);
                }
                else // (comparison > 0)
                {
                    if (!enumeratorIn.MoveNext())
                        yield break;

                    currentKeyIn = keySelectorIn(enumeratorIn.Current);
                }
            }
        }

        static public IEnumerable<T> RejectByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn)
            where T : IComparable<T>
        {
            return RejectByOrdering(enumerableOut, enumerableIn, x => x, x => x, new ComparisonComparer<T>((x, y) => x.CompareTo(y)));
        }

        static public IEnumerable<T> RejectByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Comparison<T> comparison)
        {
            return RejectByOrdering(enumerableOut, enumerableIn, x => x, x => x, new ComparisonComparer<T>(comparison));
        }

        static public IEnumerable<T> RejectByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, IComparer<T> comparer)
        {
            return RejectByOrdering(enumerableOut, enumerableIn, x => x, x => x, comparer);
        }

        static public IEnumerable<TOut> RejectByOrdering<TOut, TIn, TKey>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn)
            where TKey : IComparable<TKey>
        {
            return RejectByOrdering(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, new ComparisonComparer<TKey>((x, y) => x.CompareTo(y)));
        }

        static public IEnumerable<TOut> RejectByOrdering<TOut, TIn, TKey>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Comparison<TKey> comparison)
        {
            return RejectByOrdering(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, new ComparisonComparer<TKey>(comparison));
        }

        static public IEnumerable<TOut> RejectByOrdering<TOut, TIn, TKey>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, IComparer<TKey> comparer, bool conserveOrder = true)
        {
            return conserveOrder
                ? enumerableOut.Select((key, index) => new KeyIndexPair<TOut>(key, index)).RejectByOrderingInternal(enumerableIn, x => keySelectorOut(x.Key), keySelectorIn, comparer).OrderBy(x => x.Index).Select(x => x.Key)
                : RejectByOrderingInternal(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, comparer);
        }

        static private IEnumerable<TOut> RejectByOrderingInternal<TOut, TIn, TKey>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, IComparer<TKey> comparer)
        {
            TOut[] arrayOut = enumerableOut.ToArray();
            if (!arrayOut.Any())
                yield break;

            TIn[] arrayIn = enumerableIn.ToArray();
            if (!arrayIn.Any())
                yield break;

            IEnumerator<TOut> enumeratorOut = arrayOut.OrderBy(keySelectorOut, comparer).GetEnumerator();
            IEnumerator<TIn> enumeratorIn = arrayIn.OrderBy(keySelectorIn, comparer).GetEnumerator();

            enumeratorOut.MoveNext();
            enumeratorIn.MoveNext();
            TKey currentKeyOut = keySelectorOut(enumeratorOut.Current);
            TKey currentKeyIn = keySelectorIn(enumeratorIn.Current);

            while (true)
            {
                int comparison = comparer.Compare(currentKeyOut, currentKeyIn);

                if (comparison == 0)
                {
                    if (enumeratorIn.MoveNext())
                    {
                        while (enumeratorOut.MoveNext())
                            yield return enumeratorOut.Current;
                        yield break;
                    }

                    if (enumeratorOut.MoveNext())
                        yield break;

                    currentKeyOut = keySelectorOut(enumeratorOut.Current);
                    currentKeyIn = keySelectorIn(enumeratorIn.Current);
                }
                else if (comparison < 0)
                {
                    yield return enumeratorOut.Current;

                    if (!enumeratorOut.MoveNext())
                        yield break;

                    currentKeyOut = keySelectorOut(enumeratorOut.Current);
                }
                else // (comparison > 0)
                {
                    if (enumeratorIn.MoveNext())
                    {
                        while (enumeratorOut.MoveNext())
                            yield return enumeratorOut.Current;
                        yield break;
                    }

                    currentKeyIn = keySelectorIn(enumeratorIn.Current);
                }
            }
        }

        static public bool SetEqualsByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn)
            where T : IComparable<T>
        {
            return SetEqualsByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>((x, y) => x.CompareTo(y)));
        }
        
        static public bool SetEqualsByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Comparison<T> comparison)
        {
            return SetEqualsByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>(comparison));
        }

        static public bool SetEqualsByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, IComparer<T> comparer)
        {
            return !enumerableOut.RejectByOrdering(enumerableIn, comparer).Any();
        }

        static public bool SetDiffByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, out IEnumerable<T> added, out IEnumerable<T> removed, out IEnumerable<T> conserved, bool conserveOrder = true)
            where T : IComparable<T>
        {
            return SetDiffByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>((x, y) => x.CompareTo(y)), out added, out removed, out conserved, conserveOrder);
        }

        static public bool SetDiffByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Comparison<T> comparison, out IEnumerable<T> added, out IEnumerable<T> removed, out IEnumerable<T> conserved, bool conserveOrder = true)
        {
            return SetDiffByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>(comparison), out added, out removed, out conserved, conserveOrder);
        }

        static public bool SetDiffByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, IComparer<T> comparer, out IEnumerable<T> added, out IEnumerable<T> removed, out IEnumerable<T> conserved, bool conserveOrder = true)
        {
            if (conserveOrder)
            {
                IEnumerable<KeyIndexPair<T>> addedPair;
                IEnumerable<KeyIndexPair<T>> removedPair;
                IEnumerable<KeyIndexPair<T>> conservedPair;

                bool result = enumerableOut.Select((key, index) => new KeyIndexPair<T>(key, index)).SetDiffByOrderingInternal(enumerableIn.Select((key, index) => new KeyIndexPair<T>(key, index)), x => x.Key, x => x.Key, comparer, out addedPair, out removedPair, out conservedPair);

                added = addedPair.OrderBy(x => x.Index).Select(x => x.Key);
                removed = removedPair.OrderBy(x => x.Index).Select(x => x.Key);
                conserved = conservedPair.OrderBy(x => x.Index).Select(x => x.Key);
                return result;
            }

            return SetDiffByOrderingInternal(enumerableOut, enumerableIn, x => x, x => x, comparer, out added, out removed, out conserved);
        }

        static private bool SetDiffByOrderingInternal<TOut, TIn, TKey>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, IComparer<TKey> comparer, out IEnumerable<TIn> added, out IEnumerable<TOut> removed, out IEnumerable<TOut> conserved)
        {
            TOut[] arrayOut = enumerableOut.ToArray();
            if (!arrayOut.Any())
            {
                added = enumerableIn.ToArray();
                removed = Enumerable.Empty<TOut>();
                conserved = Enumerable.Empty<TOut>();
                return added.Any();
            }

            TIn[] arrayIn = enumerableIn.ToArray();
            if (!arrayIn.Any())
            {
                added = Enumerable.Empty<TIn>();
                removed = arrayOut;
                conserved = Enumerable.Empty<TOut>();
                return removed.Any();
            }

            IEnumerator<TOut> enumeratorOut = arrayOut.OrderBy(keySelectorOut, comparer).GetEnumerator();
            IEnumerator<TIn> enumeratorIn = arrayIn.OrderBy(keySelectorIn, comparer).GetEnumerator();

            enumeratorOut.MoveNext();
            enumeratorIn.MoveNext();
            TKey currentKeyOut = keySelectorOut(enumeratorOut.Current);
            TKey currentKeyIn = keySelectorIn(enumeratorIn.Current);

            var addedList = new List<TIn>();
            var removedList = new List<TOut>();
            var conservedList = new List<TOut>();

            while (true)
            {
                int comparison = comparer.Compare(currentKeyOut, currentKeyIn);

                if (comparison == 0)
                {
                    conservedList.Add(enumeratorOut.Current);

                    if (enumeratorOut.MoveNext())
                    {
                        while (enumeratorIn.MoveNext())
                            addedList.Add(enumeratorIn.Current);
                        break;
                    }

                    if (enumeratorIn.MoveNext())
                    {
                        while (enumeratorOut.MoveNext())
                            removedList.Add(enumeratorOut.Current);
                        break;
                    }

                    currentKeyOut = keySelectorOut(enumeratorOut.Current);
                    currentKeyIn = keySelectorIn(enumeratorIn.Current);
                }
                else if (comparison < 0)
                {
                    removedList.Add(enumeratorOut.Current);

                    if (enumeratorOut.MoveNext())
                    {
                        while (enumeratorIn.MoveNext())
                            addedList.Add(enumeratorIn.Current);
                        break;
                    }

                    currentKeyOut = keySelectorOut(enumeratorOut.Current);
                }
                else // (comparison > 0)
                {
                    addedList.Add(enumeratorIn.Current);

                    if (enumeratorIn.MoveNext())
                    {
                        while (enumeratorOut.MoveNext())
                            removedList.Add(enumeratorOut.Current);
                        break;
                    }

                    currentKeyIn = keySelectorIn(enumeratorIn.Current);
                }
            }

            added = addedList;
            removed = removedList;
            conserved = conservedList;

            return added.Any() || removed.Any();
        }

        static public IEnumerable<int> IndexesOfByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, bool conserveOrder = true)
            where T : IComparable<T>
        {
            return IndexesOfByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>((x, y) => x.CompareTo(y)), conserveOrder);
        }

        static public IEnumerable<int> IndexesOfByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Comparison<T> comparison, bool conserveOrder = true)
        {
            return IndexesOfByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>(comparison), conserveOrder);
        }

        static public IEnumerable<int> IndexesOfByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, IComparer<T> comparer, bool conserveOrder = true)
        {
            return enumerableOut.Select((x, index) => new KeyIndexPair<T>(x, index)).JoinByOrdering(enumerableIn, x => x.Key, x => x, (x, y) => x, comparer, conserveOrder).Select(x => x.Index);
        }

        private sealed class ComparisonComparer<T> : IComparer<T>
        {
            private readonly Comparison<T> _comparison;

            public ComparisonComparer(Comparison<T> comparison)
            {
                _comparison = comparison;
            }

            public int Compare(T x, T y)
            {
                return _comparison(x, y);
            }
        }

        private sealed class KeyIndexPair<T>
        {
            public T Key { get; }
            public int Index { get; }

            public KeyIndexPair(T key, int index)
            {
                Key = key;
                Index = index;
            }
        }
    }
}