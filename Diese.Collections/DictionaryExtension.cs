using System;
using System.Collections.Generic;

namespace Diese.Collections
{
    static public class DictionaryExtension
    {
        static public void AddRange<T, TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<T> enumerable, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
        {
            foreach (T item in enumerable)
                dictionary.Add(keySelector(item), valueSelector(item));
        }
    }
}