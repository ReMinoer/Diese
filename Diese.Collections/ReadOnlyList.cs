using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class ReadOnlyList<T> : IReadOnlyList<T>
    {
        protected readonly IList<T> List;
        public int Count => List.Count;
        public T this[int index] => List[index];

        static public ReadOnlyList<T> Empty => new ReadOnlyList<T>();

        private ReadOnlyList()
        {
        }

        public ReadOnlyList(IList<T> list)
        {
            List = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (List ?? Enumerable.Empty<T>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}