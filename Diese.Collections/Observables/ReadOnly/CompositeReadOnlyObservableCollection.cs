using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Diese.Collections.Observables.ReadOnly
{
    public class CompositeReadOnlyObservableCollection<T> : IReadOnlyObservableCollection<T>, IDisposable
    {
        private readonly IEnumerable<IReadOnlyObservableCollection<T>> _observableCollections;
        public int Count => _observableCollections.Sum(x => x.Count);
        
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public CompositeReadOnlyObservableCollection(IEnumerable<IReadOnlyObservableCollection<T>> observableCollections)
        {
            _observableCollections = observableCollections;

            foreach (IReadOnlyObservableCollection<T> observableCollection in _observableCollections)
            {
                observableCollection.PropertyChanged += OnPropertyChanged;
                observableCollection.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(sender, e);
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(sender, e);

        public IEnumerator<T> GetEnumerator() => _observableCollections.SelectMany(x => x).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose()
        {
            foreach (IReadOnlyObservableCollection<T> observableCollection in _observableCollections)
            {
                observableCollection.CollectionChanged -= OnCollectionChanged;
                observableCollection.PropertyChanged -= OnPropertyChanged;
            }
        }
    }
}