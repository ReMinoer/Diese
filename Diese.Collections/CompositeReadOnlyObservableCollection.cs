using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Diese.Collections
{
    public class CompositeReadOnlyObservableCollection<T> : IReadOnlyObservableCollection<T>
    {
        private readonly IEnumerable<IReadOnlyObservableCollection<T>> _observableCollections;
        public int Count => _observableCollections.Sum(x => x.Count);

        public CompositeReadOnlyObservableCollection(IEnumerable<IReadOnlyObservableCollection<T>> observableCollections)
        {
            _observableCollections = observableCollections;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                foreach (IReadOnlyObservableCollection<T> observableCollection in _observableCollections)
                    observableCollection.CollectionChanged += value;
            }
            remove
            {
                foreach (IReadOnlyObservableCollection<T> observableCollection in _observableCollections)
                    observableCollection.CollectionChanged -= value;
            }
        }

        public IEnumerator<T> GetEnumerator() => _observableCollections.SelectMany(x => x).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}