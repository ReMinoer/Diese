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
        public ObservableCollectionSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(converter, dataGetter, disposer)
        {
        }

        public ObservableCollectionSynchronizer(IReadOnlyObservableCollection<TObservedItem> reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference, converter, dataGetter, disposer)
        {
        }
    }

    public class ObservableCollectionSynchronizer<TObservedCollection, TObservedItem, TCollectedItem> : ObservableCollectionSynchronizer<TObservedCollection, ICollection<TCollectedItem>, TObservedItem, TCollectedItem>
        where TObservedCollection : class, IEnumerable<TObservedItem>, INotifyCollectionChanged
    {
        public ObservableCollectionSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(converter, dataGetter, disposer)
        {
        }

        public ObservableCollectionSynchronizer(TObservedCollection reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference, converter, dataGetter, disposer)
        {
        }
    }

    public class ObservableCollectionSynchronizer<TReference, TCollection, TReferenceItem, TCollectedItem> : ObservableCollectionSynchronizerBase<TReference, TCollection>
        where TReference : class, IEnumerable<TReferenceItem>, INotifyCollectionChanged
        where TCollection : class, ICollection<TCollectedItem>
    {
        protected readonly Func<TReferenceItem, TCollectedItem> Converter;
        protected readonly Func<TCollectedItem, TReferenceItem> DataGetter;
        protected readonly Action<TCollectedItem> Disposer;

        public ObservableCollectionSynchronizer(Func<TReferenceItem, TCollectedItem> converter, Func<TCollectedItem, TReferenceItem> dataGetter, Action<TCollectedItem> disposer = null)
        {
            Converter = converter;
            DataGetter = dataGetter;
            Disposer = disposer;
        }

        public ObservableCollectionSynchronizer(TReference reference, Func<TReferenceItem, TCollectedItem> converter, Func<TCollectedItem, TReferenceItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference)
        {
            Converter = converter;
            DataGetter = dataGetter;
            Disposer = disposer;
        }

        protected override void InitializeCollection(TCollection collection)
        {
            if (Reference != null)
                collection.AddMany(Reference.Select(Converter));
        }

        protected override void ResetCollection(TCollection collection)
        {
            TCollectedItem[] clearedItems = collection.ToArray();

            collection.Clear();

            foreach (TCollectedItem item in clearedItems)
                Disposer?.Invoke(item);
        }

        protected override void OnReferenceCollectionChanged(TCollection collection, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    collection.AddMany(e.NewItems.Cast<TReferenceItem>().Select(Converter));
                    return;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (TCollectedItem itemToRemove in e.OldItems.Cast<TReferenceItem>().Select(x => collection.First(y => EqualityComparer<TReferenceItem>.Default.Equals(DataGetter(y), x))))
                    {
                        collection.Remove(itemToRemove);
                        Disposer?.Invoke(itemToRemove);
                    }

                    return;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    collection.RemoveMany(e.OldItems.Cast<TReferenceItem>().Select(Converter));
                    collection.AddMany(e.NewItems.Cast<TReferenceItem>().Select(Converter));
                    return;
                }
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                {
                    ResetCollection(collection);
                    return;
                }
                default:
                    throw new NotSupportedException();
            }
        }
    }
}