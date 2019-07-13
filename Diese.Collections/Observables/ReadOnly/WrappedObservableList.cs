using System;

namespace Diese.Collections.Observables.ReadOnly
{
    public class WrappedObservableList<T> : ReadOnlyObservableList<T>, IWrappedObservableList<T>
    {
        private readonly Action<T> _addAction;

        public WrappedObservableList(IObservableList<T> list, Action<T> addAction)
            : base(list)
        {
            _addAction = addAction;
        }
        
        public void Add(T item) => _addAction(item);
    }
}