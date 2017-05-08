using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public struct ItemIndexPair<T>
    {
        public T Item { get; }
        public int Index { get; }

        public ItemIndexPair(T item, int index)
        {
            Item = item;
            Index = index;
        }
    }

    public enum ResultOrder
    {
        Conserved,
        Neglected
    }

    public enum ConservedKey
    {
        FirstOccurence,
        LastOccurence
    }

    static public class EnumerableExtension
    {

        static public bool Any<T>(this IEnumerable<T> enumerable, out T item)
        {
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    return true;
                }
            }

            item = default(T);
            return false;
        }

        static public bool Any<T>(this IEnumerable<T> enumerable, Predicate<T> predicate, out T item)
        {
            foreach (T obj in enumerable)
            {
                if (!predicate(obj))
                    continue;

                item = obj;
                return true;
            }

            item = default(T);
            return false;
        }

        static public bool Any<T>(this IEnumerable enumerable)
        {
            return enumerable.OfType<T>().Any();
        }

        static public bool Any<TResult>(this IEnumerable enumerable, out TResult item)
        {
            foreach (object obj in enumerable)
            {
                if (!(obj is TResult))
                    continue;

                item = (TResult)obj;
                return true;
            }

            item = default(TResult);
            return false;
        }

        static public bool Any<T>(this IEnumerable<T> enumerable, Type type)
        {
            return enumerable.Any(x => type.IsInstanceOfType(x));
        }

        static public bool Any<T>(this IEnumerable<T> enumerable, Type type, out T item)
        {
            foreach (T obj in enumerable)
            {
                if (!type.IsInstanceOfType(obj))
                    continue;

                item = obj;
                return true;
            }

            item = default(T);
            return false;
        }

        static public TResult First<TResult>(this IEnumerable enumerable)
        {
            return enumerable.OfType<TResult>().First();
        }

        static public TResult FirstOrDefault<TResult>(this IEnumerable enumerable)
        {
            return enumerable.OfType<TResult>().FirstOrDefault();
        }

        static public IEnumerable<T> NotNulls<T>(this IEnumerable<T> enumerable)
            where T : class
        {
            return enumerable.Where(x => x != null);
        }

        static public IEnumerable<T> OfType<T>(this IEnumerable<T> enumerable, Type type)
        {
            return enumerable.Where(x => type.IsInstanceOfType(x));
        }

        static public T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            return MinBy(enumerable, keySelector, (x, y) => x.CompareTo(y));
        }

        static public T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            return MinBy(enumerable, keySelector, comparer.Compare);
        }

        static public T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, Comparison<TKey> comparison)
        {
            if (enumerable == null)
                throw new InvalidOperationException();

            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                T min = enumerator.Current;
                TKey minKey = keySelector(min);

                while (enumerator.MoveNext())
                {
                    T current = enumerator.Current;
                    TKey currentKey = keySelector(current);

                    if (comparison(currentKey, minKey) >= 0)
                        continue;

                    min = current;
                    minKey = currentKey;
                }

                return min;
            }
        }

        static public T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            return MaxBy(enumerable, keySelector, (x, y) => x.CompareTo(y));
        }

        static public T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey> comparer)
        {
            return MaxBy(enumerable, keySelector, comparer.Compare);
        }

        static public T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, Comparison<TKey> comparison)
        {
            if (enumerable == null)
                throw new InvalidOperationException();

            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                T max = enumerator.Current;
                TKey maxKey = keySelector(max);

                while (enumerator.MoveNext())
                {
                    T current = enumerator.Current;
                    TKey currentKey = keySelector(current);

                    if (comparison(currentKey, maxKey) <= 0)
                        continue;

                    max = current;
                    maxKey = currentKey;
                }

                return max;
            }
        }

        static public bool AtLeast<T>(this IEnumerable<T> enumerable, int number)
        {
            if (number < 0)
                return true;

            int i = 0;
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    i++;
                    if (i >= number)
                        return true;
                }
            }

            return false;
        }

        static public bool AtMost<T>(this IEnumerable<T> enumerable, int number)
        {
            if (number < 0)
                return false;

            int i = 0;
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    i++;
                    if (i > number)
                        return false;
                }
            }

            return true;
        }

        static public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            return enumerable.ToDictionary(x => x.Key, x => x.Value);
        }

        static public Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable, ConservedKey conservedKey)
        {
            return enumerable.ToDictionary(x => x.Key, x => x.Value, conservedKey);
        }

        static public Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<Tuple<T1, T2>> enumerable)
        {
            return enumerable.ToDictionary(x => x.Item1, x => x.Item2);
        }

        static public Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<Tuple<T1, T2>> enumerable, ConservedKey conservedKey)
        {
            return enumerable.ToDictionary(x => x.Item1, x => x.Item2, conservedKey);
        }

        static public Dictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, Func<T, TValue> valueSelector, ConservedKey conservedKey)
        {
            var dictionary = new Dictionary<TKey, TValue>();

            foreach (T obj in enumerable)
            {
                TKey key = keySelector(obj);

                switch (conservedKey)
                {
                    case ConservedKey.FirstOccurence:
                        if (dictionary.ContainsKey(key))
                            dictionary.Add(key, valueSelector(obj));
                        break;
                    case ConservedKey.LastOccurence:
                        dictionary[key] = valueSelector(obj);
                        break;
                    default: throw new NotSupportedException();
                }
            }

            return dictionary;
        }

        static public HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }

        static public HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable, ConservedKey conservedKey)
        {
            var hashSet = new HashSet<T>();

            foreach (T obj in enumerable)
            {
                switch (conservedKey)
                {
                    case ConservedKey.FirstOccurence:
                        hashSet.Add(obj);
                        break;
                    case ConservedKey.LastOccurence:
                        hashSet.Remove(obj);
                        hashSet.Add(obj);
                        break;
                    default: throw new NotSupportedException();
                }
            }

            return hashSet;
        }

        static public IEnumerable<ItemIndexPair<T>> Indexed<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select((item, index) => new ItemIndexPair<T>(item, index));
        }

        static public int IndexOf<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            int index = 0;
            foreach (T obj in enumerable)
            {
                if (predicate(obj))
                    return index;
                index++;
            }

            return -1;
        }

        static public IEnumerable<int> IndexesOf<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            int i = 0;
            foreach (T obj in enumerable)
            {
                if (predicate(obj))
                    yield return i;
                i++;
            }
        }

        static public bool SetEquals<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn)
        {
            List<T> listIn = enumerableIn.ToList();
            return enumerableOut.All(item => listIn.Remove(item));
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

        static public IEnumerable<TResult> JoinByOrdering<T, TResult>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Func<T, T, TResult> resultSelector, ResultOrder resultOrder = ResultOrder.Conserved)
            where T : IComparable<T>
        {
            return JoinByOrdering(enumerableOut, enumerableIn, x => x, x => x, resultSelector, new ComparisonComparer<T>((x, y) => x.CompareTo(y)), resultOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<T, TResult>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Func<T, T, TResult> resultSelector, Comparison<T> comparison, ResultOrder resultOrder = ResultOrder.Conserved)
        {
            return JoinByOrdering(enumerableOut, enumerableIn, x => x, x => x, resultSelector, new ComparisonComparer<T>(comparison), resultOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<T, TResult>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Func<T, T, TResult> resultSelector, IComparer<T> comparer, ResultOrder resultOrder = ResultOrder.Conserved)
        {
            return JoinByOrdering(enumerableOut, enumerableIn, x => x, x => x, resultSelector, comparer, resultOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, ResultOrder resultOrder = ResultOrder.Conserved)
            where TKey : IComparable<TKey>
        {
            return JoinByOrdering(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, resultSelector, new ComparisonComparer<TKey>((x, y) => x.CompareTo(y)), resultOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, Comparison<TKey> comparison, ResultOrder resultOrder = ResultOrder.Conserved)
        {
            return JoinByOrdering(enumerableOut, enumerableIn, keySelectorOut, keySelectorIn, resultSelector, new ComparisonComparer<TKey>(comparison), resultOrder);
        }

        static public IEnumerable<TResult> JoinByOrdering<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, IComparer<TKey> comparer, ResultOrder resultOrder = ResultOrder.Conserved)
        {
            switch (resultOrder)
            {
                case ResultOrder.Conserved:
                    return enumerableOut.Indexed().JoinByOrderingInternal(enumerableIn, x => keySelectorOut(x.Item), keySelectorIn, (x, y) => new ItemIndexPair<TResult>(resultSelector(x.Item, y), x.Index), comparer).OrderBy(x => x.Index).Select(x => x.Item);
                case ResultOrder.Neglected:
                    return enumerableOut.JoinByOrderingInternal(enumerableIn, keySelectorOut, keySelectorIn, resultSelector, comparer);
                default:
                    throw new NotSupportedException();
            }
        }

        static private IEnumerable<TResult> JoinByOrderingInternal<TOut, TIn, TKey, TResult>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, Func<TOut, TIn, TResult> resultSelector, IComparer<TKey> comparer)
        {
            TOut[] arrayOut = enumerableOut.ToArray();
            if (!arrayOut.Any())
                yield break;

            TIn[] arrayIn = enumerableIn.ToArray();
            if (!arrayIn.Any())
                yield break;

            using (IEnumerator<TOut> enumeratorOut = arrayOut.OrderBy(keySelectorOut, comparer).GetEnumerator())
            using (IEnumerator<TIn> enumeratorIn = arrayIn.OrderBy(keySelectorIn, comparer).GetEnumerator())
            {
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

        static public IEnumerable<TOut> RejectByOrdering<TOut, TIn, TKey>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, IComparer<TKey> comparer, ResultOrder resultOrder = ResultOrder.Conserved)
        {
            switch (resultOrder)
            {
                case ResultOrder.Conserved:
                    return enumerableOut.Indexed().RejectByOrderingInternal(enumerableIn, x => keySelectorOut(x.Item), keySelectorIn, comparer).OrderBy(x => x.Index).Select(x => x.Item);
                case ResultOrder.Neglected:
                    return enumerableOut.RejectByOrderingInternal(enumerableIn, keySelectorOut, keySelectorIn, comparer);
                default:
                    throw new NotSupportedException();
            }
        }

        static private IEnumerable<TOut> RejectByOrderingInternal<TOut, TIn, TKey>(this IEnumerable<TOut> enumerableOut, IEnumerable<TIn> enumerableIn, Func<TOut, TKey> keySelectorOut, Func<TIn, TKey> keySelectorIn, IComparer<TKey> comparer)
        {
            TOut[] arrayOut = enumerableOut.ToArray();
            if (!arrayOut.Any())
                yield break;

            TIn[] arrayIn = enumerableIn.ToArray();
            if (!arrayIn.Any())
                yield break;

            using (IEnumerator<TOut> enumeratorOut = arrayOut.OrderBy(keySelectorOut, comparer).GetEnumerator())
            using (IEnumerator<TIn> enumeratorIn = arrayIn.OrderBy(keySelectorIn, comparer).GetEnumerator())
            {
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

        static public bool SetDiffByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, out IEnumerable<T> added, out IEnumerable<T> removed, out IEnumerable<T> conserved, ResultOrder resultOrder = ResultOrder.Conserved)
            where T : IComparable<T>
        {
            return SetDiffByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>((x, y) => x.CompareTo(y)), out added, out removed, out conserved, resultOrder);
        }

        static public bool SetDiffByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Comparison<T> comparison, out IEnumerable<T> added, out IEnumerable<T> removed, out IEnumerable<T> conserved, ResultOrder resultOrder = ResultOrder.Conserved)
        {
            return SetDiffByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>(comparison), out added, out removed, out conserved, resultOrder);
        }

        static public bool SetDiffByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, IComparer<T> comparer, out IEnumerable<T> added, out IEnumerable<T> removed, out IEnumerable<T> conserved, ResultOrder resultOrder = ResultOrder.Conserved)
        {
            switch (resultOrder)
            {
                case ResultOrder.Conserved:
                    IEnumerable<ItemIndexPair<T>> addedPair;
                    IEnumerable<ItemIndexPair<T>> removedPair;
                    IEnumerable<ItemIndexPair<T>> conservedPair;

                    bool result = enumerableOut.Indexed().SetDiffByOrderingInternal(enumerableIn.Indexed(), x => x.Item, x => x.Item, comparer, out addedPair, out removedPair, out conservedPair);

                    added = addedPair.OrderBy(x => x.Index).Select(x => x.Item);
                    removed = removedPair.OrderBy(x => x.Index).Select(x => x.Item);
                    conserved = conservedPair.OrderBy(x => x.Index).Select(x => x.Item);
                    return result;
                case ResultOrder.Neglected:
                    return enumerableOut.SetDiffByOrderingInternal(enumerableIn, x => x, x => x, comparer, out added, out removed, out conserved);
                default:
                    throw new NotSupportedException();
            }
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

            using (IEnumerator<TOut> enumeratorOut = arrayOut.OrderBy(keySelectorOut, comparer).GetEnumerator())
            using (IEnumerator<TIn> enumeratorIn = arrayIn.OrderBy(keySelectorIn, comparer).GetEnumerator())
            {

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
            }

            return added.Any() || removed.Any();
        }

        static public IEnumerable<int> IndexesOfByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn)
            where T : IComparable<T>
        {
            return IndexesOfByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>((x, y) => x.CompareTo(y)));
        }

        static public IEnumerable<int> IndexesOfByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, Comparison<T> comparison)
        {
            return IndexesOfByOrdering(enumerableOut, enumerableIn, new ComparisonComparer<T>(comparison));
        }

        static public IEnumerable<int> IndexesOfByOrdering<T>(this IEnumerable<T> enumerableOut, IEnumerable<T> enumerableIn, IComparer<T> comparer)
        {
            return enumerableOut.Indexed().JoinByOrdering(enumerableIn, x => x.Item, x => x, (x, y) => x, comparer, ResultOrder.Neglected).Select(x => x.Index).OrderBy(x => x);
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
    }
}