using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    static public class ListExtension
    {
        static public int Replace<T>(this IList<T> list, T oldItem, T newItem)
        {
            int index = list.IndexOf(oldItem);
            if (index == -1)
                return -1;

            list.RemoveAt(index);
            list.Insert(index, newItem);
            return index;
        }

        static public int Replace<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            int index = list.IndexOf(predicate);
            if (index == -1)
                return -1;

            list.RemoveAt(index);
            list.Insert(index, newItem);
            return index;
        }

        static public IEnumerable<int> ReplaceAll<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            int[] indexes = list.IndexesOf(predicate).ToArray();

            foreach (int index in indexes)
            {
                list.RemoveAt(index);
                list.Insert(index, newItem);
            }

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
                list.RemoveAt(index);
                list.Insert(index, newItem);
            }

            return indexes;
        }

        static public int ReplaceOrAdd<T>(this IList<T> list, T oldItem, T newItem)
        {
            bool added;
            return ReplaceOrAdd(list, oldItem, newItem, out added);
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

            list.RemoveAt(index);
            list.Insert(index, newItem);
            added = false;
            return index;
        }

        static public int ReplaceOrAdd<T>(this IList<T> list, Predicate<T> predicate, T newItem)
        {
            bool added;
            return ReplaceOrAdd(list, predicate, newItem, out added);
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

            list.RemoveAt(index);
            list.Insert(index, newItem);
            added = false;
            return index;
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
    }
}