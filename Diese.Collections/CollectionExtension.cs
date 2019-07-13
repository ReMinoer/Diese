using System;
using System.Collections.Generic;
using System.Linq;
using Diese.Collections.ReadOnly;

namespace Diese.Collections
{
    static public class CollectionExtension
    {
        static public void AddMany<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
                collection.Add(item);
        }

        static public bool RemoveMany<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            return items.Aggregate(true, (current, item) => current & collection.Remove(item));
        }

        static public T FirstOrAdd<T>(this ICollection<T> collection, Predicate<T> predicate, Func<T> itemFactory)
        {
            if (collection.Any(predicate, out T firstItem))
                return firstItem;

            T item = itemFactory();
            collection.Add(item);
            return item;
        }

        static public TItem FirstOrAdd<T, TItem>(this ICollection<T> collection, Func<TItem> itemFactory)
            where TItem : T
        {
            if (collection.AnyOfType(out TItem firstItem))
                return firstItem;

            TItem item = itemFactory();
            collection.Add(item);
            return item;
        }

        static public bool Remove<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            return collection.Any(predicate, out T item) && collection.Remove(item);
        }

        static public void RemoveAll<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            IEnumerable<T> toRemove = collection.Where(x => predicate(x));
            foreach (T item in toRemove)
                collection.Remove(item);
        }

        static public ReadOnlyCollection<T> AsReadOnly<T>(this ICollection<T> collection)
        {
            return new ReadOnlyCollection<T>(collection);
        }
    }
}