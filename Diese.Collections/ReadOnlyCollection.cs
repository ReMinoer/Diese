using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class ReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection<T> _collection;
        public int Count => _collection.Count;

        static public ReadOnlyCollection<T> Empty => new ReadOnlyCollection<T>();

        private ReadOnlyCollection()
        {
        }

        public ReadOnlyCollection(ICollection<T> collection)
        {
            _collection = collection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (_collection ?? Enumerable.Empty<T>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}