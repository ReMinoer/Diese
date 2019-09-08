using System;
using System.Collections.Specialized;
using Diese.Collections.Observables;

namespace Diese.Collections.Utils
{
    public class CollectionChangeDelayer<T> : IDisposable
    {
        private readonly IObservableCollection<T> _collection;
        private NotifyCollectionChangedEventArgs _lastChange;

        public CollectionChangeDelayer(IObservableCollection<T> collection)
        {
            _collection = collection;
            _collection.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_lastChange != null)
                throw new InvalidOperationException();

            _lastChange = e;
        }

        public NotifyCollectionChangedEventArgs GetLastChange()
        {
            if (_lastChange == null)
                throw new InvalidOperationException();

            NotifyCollectionChangedEventArgs result = _lastChange;
            _lastChange = null;
            return result;
        }

        public bool GetCountChange() => (_lastChange ?? throw new InvalidOperationException()).Action != NotifyCollectionChangedAction.Replace;

        public void Dispose()
        {
            _collection.CollectionChanged -= OnCollectionChanged;
        }
    }
}