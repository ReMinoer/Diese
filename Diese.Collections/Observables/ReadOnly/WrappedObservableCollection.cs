using System;

namespace Diese.Collections.Observables.ReadOnly
{
    public class WrappedObservableCollection<T> : ReadOnlyObservableCollection<T>, IWrappedObservableCollection<T>
    {
        private readonly Action<T> _addAction;

        public WrappedObservableCollection(IObservableCollection<T> collection, Action<T> addAction)
            : base(collection)
        {
            _addAction = addAction;
        }

        public void Add(T item) => _addAction(item);
    }
}