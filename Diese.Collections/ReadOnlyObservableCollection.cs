using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Diese.Collections
{
    public class ReadOnlyObservableCollection<T> : IReadOnlyObservableCollection<T>
    {
        private readonly ObservableCollection<T> _observableCollection;
        public int Count => _observableCollection.Count;

        public ReadOnlyObservableCollection(ObservableCollection<T> observableCollection)
        {
            _observableCollection = observableCollection;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => _observableCollection.CollectionChanged += value;
            remove => _observableCollection.CollectionChanged -= value;
        }

        public IEnumerator<T> GetEnumerator() => _observableCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_observableCollection).GetEnumerator();
    }
}