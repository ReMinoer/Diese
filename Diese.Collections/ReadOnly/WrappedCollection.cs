using System;
using System.Collections.Generic;

namespace Diese.Collections.ReadOnly
{
    public class WrappedCollection<T> : ReadOnlyCollection<T>, IWrappedCollection<T>
    {
        private readonly Action<T> _addAction;

        public WrappedCollection(ICollection<T> collection)
            : base(collection)
        {
            _addAction = collection.Add;
        }

        public WrappedCollection(ICollection<T> collection, Action<T> addAction)
            : base(collection)
        {
            _addAction = addAction;
        }

        public void Add(T item) => _addAction(item);
    }
}