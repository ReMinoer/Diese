using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Diese.Collections.ReadOnly;

namespace Diese.Collections
{
    static public class ListExtension
    {
        static public void InsertMany<T>(this IList<T> list, int index, IEnumerable<T> items)
        {
            foreach (T item in items)
                list.Insert(index++, item);
        }

        static public int Replace<T>(this IList<T> list, T oldItem, T newItem)
        {
            int index = list.IndexOf(oldItem);
            if (index == -1)
                return -1;

            list[index] = newItem;
            return index;
        }

        static public int Replace<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            int index = list.IndexOf(predicate);
            if (index == -1)
                return -1;

            list[index] = newItem;
            return index;
        }

        static public void ReplaceRange<T>(this IList<T> list, int index, IEnumerable<T> newItems)
        {
            foreach (T newItem in newItems)
                list[index++] = newItem;
        }

        static public IEnumerable<int> ReplaceAll<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            int[] indexes = list.IndexesOf(predicate).ToArray();

            foreach (int index in indexes)
                list[index] = newItem;

            return indexes;
        }

        static public IEnumerable<int> ReplaceAll<T>(this IList<T> list, Predicate<T> predicate, Func<T> newItemsFactory)
        {
            return ReplaceAll(list, predicate, (oldItem, index) => newItemsFactory());
        }

        static public IEnumerable<int> ReplaceAll<T>(this IList<T> list, Predicate<T> predicate, Func<T, T> newItemsFactory)
        {
            return ReplaceAll(list, predicate, (oldItem, index) => newItemsFactory(oldItem));
        }

        static public IEnumerable<int> ReplaceAll<T>(this IList<T> list, Predicate<T> predicate, Func<T, int, T> newItemsFactory)
        {
            int[] indexes = list.IndexesOf(predicate).ToArray();

            foreach (int index in indexes)
            {
                T newItem = newItemsFactory(list[index], index);
                list[index] = newItem;
            }

            return indexes;
        }

        static public int ReplaceOrAdd<T>(this IList<T> list, T oldItem, T newItem)
        {
            return ReplaceOrAdd(list, oldItem, newItem, out bool _);
        }

        static public int ReplaceOrAdd<T>(this IList<T> list, T oldItem, T newItem, out bool added)
        {
            int index = list.IndexOf(oldItem);
            if (index == -1)
            {
                list.Add(newItem);
                added = true;
                return list.Count - 1;
            }

            list[index] = newItem;
            added = false;
            return index;
        }

        static public int ReplaceOrAdd<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            return ReplaceOrAdd(list, predicate, newItem, out bool _);
        }

        static public int ReplaceOrAdd<T>(this IList<T> list, Predicate<T> predicate, T newItem, out bool added)
        {
            int index = list.IndexOf(predicate);
            if (index == -1)
            {
                list.Add(newItem);
                added = true;
                return list.Count - 1;
            }

            list[index] = newItem;
            added = false;
            return index;
        }

        static public void Move<T>(this IList<T> list, int oldIndex, int newIndex)
        {
            T item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
        }

        static public bool Move<T>(this IList<T> list, T item, int newIndex)
        {
            if (!list.Remove(item))
                return false;

            list.Insert(newIndex, item);
            return true;
        }

        static public bool MoveMany<T>(this IList<T> list, IEnumerable<T> items, int index)
        {
            T[] itemsArray = items.ToArray();
            int[] oldIndices = itemsArray.Select(list.IndexOf).OrderByDescending(x => x).ToArray();
            if (oldIndices.Any(x => x == -1))
                return false;

            foreach (int oldIndex in oldIndices)
                list.RemoveAt(oldIndex);

            list.InsertMany(index, itemsArray);
            return true;
        }

        static public bool Remove<T>(this IList<T> list, Predicate<T> predicate)
        {
            int index = list.IndexOf(predicate);
            if (index == -1)
                return false;

            list.RemoveAt(index);
            return true;
        }

        static public void RemoveAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            IEnumerable<int> indexes = list.IndexesOf(predicate);
            foreach (int index in indexes)
                list.RemoveAt(index);
        }

        static public ReadOnlyList<T> AsReadOnly<T>(this IList<T> list)
        {
            return new ReadOnlyList<T>(list);
        }
        
        static public void AddMany(this IList list, IEnumerable items)
        {
            foreach (object item in items)
                list.Add(item);
        }

        static public void RemoveMany(this IList list, IEnumerable items)
        {
            foreach (object item in items)
                list.Remove(item);
        }

        static public void InsertMany(this IList list, int index, IEnumerable items)
        {
            foreach (object item in items)
                list.Insert(index++, item);
        }

        static public void ReplaceRange(this IList list, int index, IEnumerable newItems)
        {
            foreach (object newItem in newItems)
                list[index++] = newItem;
        }

        static public void MoveMany(this IList list, IEnumerable items, int index)
        {
            using (IEnumerator<object> enumerator = list.Cast<object>().OrderBy(list.IndexOf).GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return;

                object item = enumerator.Current;
                int itemIndex = list.IndexOf(item);

                list.RemoveAt(itemIndex);
                list.Insert(index, item);

                while (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    itemIndex = list.IndexOf(item);
                    if (itemIndex > index)
                        index++;

                    list.RemoveAt(itemIndex);
                    list.Insert(index, item);
                }
            }
        }
    }
}