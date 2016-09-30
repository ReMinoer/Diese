using System.Collections;
using System.Collections.Generic;

namespace Diese.Collections
{
    public class ReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection<T> _collection;
        public int Count => _collection.Count;

        public ReadOnlyCollection(ICollection<T> collection)
        {
            _collection = collection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}