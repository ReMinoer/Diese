using System;
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
        private bool _enumeratorUsed;

        public Enumerable(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_enumeratorUsed && _enumerator.MoveNext())
                throw new InvalidOperationException("Cannot handle simultaneous enumeration.");

            _enumerator.Reset();
            _enumeratorUsed = true;

            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}