using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class ReadOnlyEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;

        static public ReadOnlyEnumerable<T> Empty => new ReadOnlyEnumerable<T>();

        private ReadOnlyEnumerable()
        {
        }

        public ReadOnlyEnumerable(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (_enumerable ?? Enumerable.Empty<T>()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}