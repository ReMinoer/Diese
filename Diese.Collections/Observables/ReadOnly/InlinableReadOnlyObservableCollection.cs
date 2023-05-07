using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Diese.Collections.Observables.ReadOnly
{
    public class InlinableReadOnlyObservableCollection<T> : IReadOnlyObservableCollection<T>, IDisposable
    {
        private readonly IReadOnlyObservableCollection<T> _collection;
        private readonly Func<T, IReadOnlyObservableCollection<T>> _inlining;
        private IReadOnlyObservableCollection<T> _inlinedCollection;

        protected readonly ObservableCollection<T> ObservableCollection;
        private readonly INotifyPropertyChanged _notifyPropertyChanged;

        public bool IsInlined => _inlinedCollection != null;
        public T InlinedItem { get; private set; }
        public event EventHandler IsInlinedChanged;

        public int Count => ObservableCollection.Count;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _notifyPropertyChanged.PropertyChanged += value;
            remove => _notifyPropertyChanged.PropertyChanged -= value;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add => ObservableCollection.CollectionChanged += value;
            remove => ObservableCollection.CollectionChanged -= value;
        }

        public InlinableReadOnlyObservableCollection(IReadOnlyObservableCollection<T> collection, Func<T, IReadOnlyObservableCollection<T>> inlining)
        {
            _collection = collection;
            _inlining = inlining;

            ObservableCollection = new ObservableCollection<T>();
            _notifyPropertyChanged = ObservableCollection;

            if (CanInline(out IReadOnlyObservableCollection<T> inlinedCollection))
                StartInline(inlinedCollection);
            else
                ObservableCollection.AddMany(collection);
            
            _collection.CollectionChanged += OnCollectionChanged;
        }

        public void Dispose()
        {
            _collection.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!IsInlined && CanInline(out IReadOnlyObservableCollection<T> inlinedCollection))
            {
                StartInline(inlinedCollection);
                return;
            }

            if (IsInlined && !CanInline(out _))
            {
                StopInline();
                return;
            }

            UpdateObservableCollection(ObservableCollection, e);
        }

        private bool CanInline(out IReadOnlyObservableCollection<T> inlinedCollection)
        {
            if (_collection.Count != 1)
            {
                inlinedCollection = null;
                return false;
            }

            inlinedCollection = _inlining(Enumerable.First(_collection));
            return inlinedCollection != null;
        }

        private void StartInline(IReadOnlyObservableCollection<T> inlinedCollection)
        {
            ObservableCollection.Clear();
            ObservableCollection.AddMany(inlinedCollection);
            
            InlinedItem = Enumerable.First(_collection);

            _inlinedCollection = inlinedCollection;
            _inlinedCollection.CollectionChanged += OnInlinedCollectionChanged;

            IsInlinedChanged?.Invoke(this, EventArgs.Empty);
        }

        private void StopInline()
        {
            if (_inlinedCollection is null)
                return;

            _inlinedCollection.CollectionChanged -= OnInlinedCollectionChanged;
            _inlinedCollection = null;
            
            InlinedItem = default(T);

            ObservableCollection.Clear();
            ObservableCollection.AddMany(ObservableCollection);

            IsInlinedChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnInlinedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateObservableCollection(ObservableCollection, e);
        }

        protected virtual void UpdateObservableCollection(ObservableCollection<T> observableCollection, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    observableCollection.AddMany(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    observableCollection.RemoveMany(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    observableCollection.RemoveMany(e.OldItems);
                    observableCollection.AddMany(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    observableCollection.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public IEnumerator<T> GetEnumerator() => ObservableCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}