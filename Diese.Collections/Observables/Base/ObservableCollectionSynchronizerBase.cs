using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Diese.Collections.ReadOnly;

namespace Diese.Collections.Observables.Base
{
    public abstract class ObservableCollectionSynchronizerBase<TReference, TCollection> : IDisposable
        where TReference : class, INotifyCollectionChanged
    {
        private readonly List<TCollection> _collections;
        public IReadOnlyCollection<TCollection> Collections { get; }

        private TReference _reference;
        public TReference Reference
        {
            get => _reference;
            set
            {
                if (_reference == value)
                    return;

                if (_reference != null)
                {
                    _reference.CollectionChanged -= OnReferenceCollectionChanged;

                    foreach (TCollection collection in _collections)
                        ResetCollection(collection);
                }

                _reference = value;

                if (_reference != null)
                {
                    foreach (TCollection collection in _collections)
                        InitializeCollection(collection);

                    Reference.CollectionChanged += OnReferenceCollectionChanged;
                }
            }
        }

        public ObservableCollectionSynchronizerBase()
        {
            _collections = new List<TCollection>();
            Collections = new ReadOnlyCollection<TCollection>(_collections);
        }

        public ObservableCollectionSynchronizerBase(TReference reference)
            : this()
        {
            Reference = reference;
        }
        
        protected abstract void InitializeCollection(TCollection collection);
        protected abstract void ResetCollection(TCollection collection);
        protected abstract void OnReferenceCollectionChanged(TCollection collection, NotifyCollectionChangedEventArgs e);

        private void OnReferenceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (TCollection collection in _collections)
                OnReferenceCollectionChanged(collection, e);
        }

        public void Subscribe(TCollection collection)
        {
            _collections.Add(collection);

            ResetCollection(collection);
            InitializeCollection(collection);
        }

        public void Unsubscribe(TCollection collection)
        {
            _collections.Remove(collection);
        }

        public void UnsubscribeAll()
        {
            _collections.Clear();
        }

        public void Dispose()
        {
            if (Reference != null)
                Reference.CollectionChanged -= OnReferenceCollectionChanged;

            UnsubscribeAll();
        }
    }
}