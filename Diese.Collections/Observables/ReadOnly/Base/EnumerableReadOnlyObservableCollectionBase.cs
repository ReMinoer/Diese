using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables.ReadOnly.Base
{
    public abstract class EnumerableReadOnlyObservableCollectionBase<TEnumerable, TItem> : IReadOnlyObservableCollection<TItem>, IDisposable
        where TEnumerable : IEnumerable
    {
        protected readonly TEnumerable Enumerable;
        private readonly INotifyCollectionChanged _notifier;

        private int _count;
        public int Count
        {
            get => _count;
            private set
            {
                if (_count == value)
                    return;

                _count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public EnumerableReadOnlyObservableCollectionBase(TEnumerable enumerable)
        {
            Enumerable = enumerable;
            Count = Enumerable.Count();

            _notifier = enumerable as INotifyCollectionChanged;
            if (_notifier != null)
                _notifier.CollectionChanged += OnCollectionChanged;
        }
        
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Count += e.NewItems.Count;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Count -= e.OldItems.Count;
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Count = 0;
                    break;
            }

            CollectionChanged?.Invoke(this, e);
        }

        public abstract IEnumerator<TItem> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dispose()
        {
            if (_notifier != null)
                _notifier.CollectionChanged -= OnCollectionChanged;
        }
    }
}