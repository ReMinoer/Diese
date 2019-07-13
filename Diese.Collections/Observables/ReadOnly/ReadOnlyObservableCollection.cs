using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables.ReadOnly
{
    public class ReadOnlyObservableCollection<T> : IReadOnlyObservableCollection<T>, IDisposable
    {
        private readonly IObservableCollection<T> _observableCollection;
        public int Count => _observableCollection.Count;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ReadOnlyObservableCollection(IObservableCollection<T> observableCollection)
        {
            _observableCollection = observableCollection;
            _observableCollection.PropertyChanged += OnPropertyChanged;
            _observableCollection.CollectionChanged += OnCollectionChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(sender, e);
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(sender, e);

        public IEnumerator<T> GetEnumerator() => _observableCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_observableCollection).GetEnumerator();

        public void Dispose()
        {
            _observableCollection.CollectionChanged -= OnCollectionChanged;
            _observableCollection.PropertyChanged -= OnPropertyChanged;
        }
    }
}