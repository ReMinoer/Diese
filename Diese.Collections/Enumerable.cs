using System.Collections;
using System.Collections.Generic;

namespace Diese.Collections
{
    public class Enumerable<T> : IEnumerable<T>
    {
        static public IEnumerable<T> New(T item)
        {
            yield return item;
        }

        private readonly IEnumerator<T> _enumerator;

        public Enumerable(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}