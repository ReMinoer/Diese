using System;
using System.Collections.Generic;

namespace Diese.Collections
{
    static public class DictionaryExtension
    {
        static public bool Any<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Predicate<TKey> predicate, out TValue item)
        {
            foreach (KeyValuePair<TKey, TValue> obj in dictionary)
            {
                if (!predicate(obj.Key))
                    continue;

                item = obj.Value;
                return true;
            }

            item = default(TValue);
            return false;
        }
        
        static public void AddRange<T, TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<T> enumerable, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
        {
            foreach (T item in enumerable)
                dictionary.Add(keySelector(item), valueSelector(item));
        }
    }
}
