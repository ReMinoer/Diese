using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Diese.Collections.Observables.ReadOnly.Base
{
    public abstract class CompositeReadOnlyObservableCollectionBase<T, TCollection> : IReadOnlyObservableCollection<T>, IDisposable
        where TCollection : IReadOnlyObservableCollection<T>
    {
        protected readonly TCollection[] ObservableCollections;

        public int Count => ObservableCollections.Sum(x => x.Count);
        
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected CompositeReadOnlyObservableCollectionBase(TCollection[] observableCollections)
        {
            ObservableCollections = observableCollections;

            foreach (TCollection observableCollection in ObservableCollections)
            {
                observableCollection.PropertyChanged += OnPropertyChanged;
                observableCollection.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs eventArgs = BuildCompositeEventArgs((TCollection)sender, e);
            if (eventArgs != null)
                CollectionChanged?.Invoke(this, eventArgs);
        }

        protected abstract NotifyCollectionChangedEventArgs BuildCompositeEventArgs(TCollection sender, NotifyCollectionChangedEventArgs e);

        public IEnumerator<T> GetEnumerator() => ObservableCollections.SelectMany(x => x).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose()
        {
            foreach (TCollection observableCollection in ObservableCollections)
            {
                observableCollection.CollectionChanged -= OnCollectionChanged;
                observableCollection.PropertyChanged -= OnPropertyChanged;
            }
        }
    }
}