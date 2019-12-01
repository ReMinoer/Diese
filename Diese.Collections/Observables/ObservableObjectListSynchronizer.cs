using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.Base;
using Diese.Collections.Observables.ReadOnly;

namespace Diese.Collections.Observables
{
    public class ObservableObjectListSynchronizer<TObservedItem, TCollectedItem> : ObservableObjectListSynchronizer<IReadOnlyObservableList<TObservedItem>, TObservedItem, TCollectedItem>
    {
        public ObservableObjectListSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(converter, dataGetter, disposer)
        {
        }

        public ObservableObjectListSynchronizer(IReadOnlyObservableList<TObservedItem> reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference, converter, dataGetter, disposer)
        {
        }
    }

    public class ObservableObjectListSynchronizer<TObservedList, TObservedItem, TCollectedItem> : ObservableObjectListSynchronizer<TObservedList, IList, TObservedItem, TCollectedItem>
        where TObservedList : class, IEnumerable<TObservedItem>, INotifyCollectionChanged
    {
        public ObservableObjectListSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(converter, dataGetter, disposer)
        {
        }

        public ObservableObjectListSynchronizer(TObservedList reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference, converter, dataGetter, disposer)
        {
        }
    }

    public class ObservableObjectListSynchronizer<TObservedList, TCollectedCollection, TObservedItem, TCollectedItem> : ObservableCollectionSynchronizerBase<TObservedList, TCollectedCollection>
        where TObservedList : class, IEnumerable<TObservedItem>, INotifyCollectionChanged
        where TCollectedCollection : class, IList
    {
        protected readonly Func<TObservedItem, TCollectedItem> Converter;
        protected readonly Func<TCollectedItem, TObservedItem> DataGetter;
        protected readonly Action<TCollectedItem> Disposer;

        public ObservableObjectListSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
        {
            Converter = converter;
            DataGetter = dataGetter;
            Disposer = disposer;
        }

        public ObservableObjectListSynchronizer(TObservedList reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference)
        {
            Converter = converter;
            DataGetter = dataGetter;
            Disposer = disposer;
        }

        protected override void InitializeCollection(TCollectedCollection list)
        {
            if (Reference != null)
                list.AddMany(Reference.Select(Converter));
        }

        protected override void ResetCollection(TCollectedCollection list)
        {
            object[] clearedItems = list.Cast<object>().ToArray();

            list.Clear();

            foreach (TCollectedItem item in clearedItems)
                Disposer?.Invoke(item);
        }

        protected override void OnReferenceCollectionChanged(TCollectedCollection list, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    list.InsertMany(e.NewStartingIndex, e.NewItems.Cast<TObservedItem>().Select(Converter));
                    return;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (TCollectedItem itemToRemove in e.OldItems.Cast<TObservedItem>().Select(x => list.OfType<TCollectedItem>().First(y => EqualityComparer<TObservedItem>.Default.Equals(DataGetter(y), x))))
                    {
                        list.Remove(itemToRemove);
                        Disposer?.Invoke(itemToRemove);
                    }

                    return;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    list.ReplaceRange(e.OldStartingIndex, e.NewStartingIndex, e.NewItems.Cast<TObservedItem>().Select(Converter));
                    return;
                }
                case NotifyCollectionChangedAction.Move:
                {
                    list.MoveMany(e.NewItems.Cast<TObservedItem>().Select(Converter), e.NewStartingIndex);
                    return;
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    ResetCollection(list);
                    return;
                }
                default:
                    throw new NotSupportedException();
            }
        }
    }
}