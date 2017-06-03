using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class ReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IList<T> _list;
        public int Count => _list.Count;
        public T this[int index] => _list[index];

        static public ReadOnlyList<T> Empty => new ReadOnlyList<T>();

        private ReadOnlyList()
        {
        }

        public ReadOnlyList(IList<T> list)
        {
            _list = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (_list ?? Enumerable.Empty<T>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}