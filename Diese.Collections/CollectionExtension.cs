using System;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    static public class CollectionExtension
    {
        static public T FirstOrAdd<T>(this ICollection<T> collection, Predicate<T> predicate, Func<T> itemFactory)
        {
            T firstItem;
            if (collection.Any(predicate, out firstItem))
                return firstItem;

            T item = itemFactory();
            collection.Add(item);
            return item;
        }

        static public TItem FirstOrAdd<T, TItem>(this ICollection<T> collection, Func<TItem> itemFactory)
            where TItem : T
        {
            TItem firstItem;
            if (collection.Any(out firstItem))
                return firstItem;

            TItem item = itemFactory();
            collection.Add(item);
            return item;
        }

        static public bool Remove<T>(this ICollection<T> collection, Predicate<T> predicate)
        {
            T item;
            return collection.Any(predicate, out item) && collection.Remove(item);
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