using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.Base;
using Diese.Collections.Observables.ReadOnly;

namespace Diese.Collections.Observables
{
    public class ObservableCollectionSynchronizer<TObservedItem, TCollectedItem> : ObservableCollectionSynchronizer<IReadOnlyObservableCollection<TObservedItem>, TObservedItem, TCollectedItem>
    {
        public ObservableCollectionSynchronizer(Func<TObservedItem, TCollectedItem> converter)
            : base(converter)
        {
        }

        public ObservableCollectionSynchronizer(IReadOnlyObservableCollection<TObservedItem> reference, Func<TObservedItem, TCollectedItem> converter)
            : base(reference, converter)
        {
        }
    }

    public class ObservableCollectionSynchronizer<TObservedCollection, TObservedItem, TCollectedItem> : ObservableCollectionSynchronizerBase<TObservedCollection, ICollection<TCollectedItem>>
        where TObservedCollection : class, IEnumerable<TObservedItem>, INotifyCollectionChanged
    {
        private readonly Func<TObservedItem, TCollectedItem> _converter;

        public ObservableCollectionSynchronizer(Func<TObservedItem, TCollectedItem> converter)
        {
            _converter = converter;
        }

        public ObservableCollectionSynchronizer(TObservedCollection reference, Func<TObservedItem, TCollectedItem> converter)
            : base(reference)
        {
            _converter = converter;
        }

        protected override void InitializeCollection(ICollection<TCollectedItem> collection)
        {
            if (Reference != null)
                collection.AddMany(Reference.Select(_converter));
        }

        protected override void ResetCollection(ICollection<TCollectedItem> collection)
        {
            collection.Clear();
        }

        protected override void OnReferenceCollectionChanged(ICollection<TCollectedItem> collection, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    collection.AddMany(e.NewItems.Cast<TObservedItem>().Select(_converter));
                    return;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    collection.RemoveMany(e.OldItems.Cast<TObservedItem>().Select(_converter));
                    return;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    collection.RemoveMany(e.OldItems.Cast<TObservedItem>().Select(_converter));
                    collection.AddMany(e.NewItems.Cast<TObservedItem>().Select(_converter));
                    return;
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    collection.Clear();
                    return;
                }
            }
        }
    }
}