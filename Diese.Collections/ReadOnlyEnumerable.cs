using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class ReadOnlyEnumerable : IEnumerable
    {
        static public ReadOnlyEnumerable Empty => new ReadOnlyEnumerable();
        private readonly IEnumerable _enumerable;

        private ReadOnlyEnumerable()
        {
        }

        public ReadOnlyEnumerable(IEnumerable enumerable)
        {
            _enumerable = enumerable;
        }

        public IEnumerator GetEnumerator()
        {
            return (_enumerable ?? Enumerable.Empty<object>()).GetEnumerator();
        }
    }

    public class ReadOnlyEnumerable<T> : IEnumerable<T>
    {
        static public ReadOnlyEnumerable<T> Empty => new ReadOnlyEnumerable<T>();
        private readonly IEnumerable<T> _enumerable;

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