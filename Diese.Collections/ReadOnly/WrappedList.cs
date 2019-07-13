using System;
using System.Collections.Generic;

namespace Diese.Collections.ReadOnly
{
    public class WrappedList<T> : ReadOnlyList<T>, IWrappedList<T>
    {
        private readonly Action<T> _addAction;

        public WrappedList(IList<T> list)
            : base(list)
        {
            _addAction = list.Add;
        }

        public WrappedList(IList<T> list, Action<T> addAction)
            : base(list)
        {
            _addAction = addAction;
        }
        
        public void Add(T item) => _addAction(item);
    }
}