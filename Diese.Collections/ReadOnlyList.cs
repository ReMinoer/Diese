using System.Collections;
using System.Collections.Generic;

namespace Diese.Collections
{
    public class ReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IList<T> _list;
        public int Count => _list.Count;
        public T this[int index] => _list[index];

        public ReadOnlyList(IList<T> list)
        {
            _list = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}