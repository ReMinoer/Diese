using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections.ReadOnly
{
    public class ReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        protected readonly ICollection<T> Collection;
        public int Count => Collection.Count;

        static public ReadOnlyCollection<T> Empty => new ReadOnlyCollection<T>();

        private ReadOnlyCollection()
        {
        }

        public ReadOnlyCollection(ICollection<T> collection)
        {
            Collection = collection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Collection ?? Enumerable.Empty<T>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}