using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.ReadOnly;

namespace Diese.Collections.Observables
{
    public class ObservableListSynchronizer<TObservedItem, TCollectedItem> : ObservableListSynchronizer<IReadOnlyObservableList<TObservedItem>, TObservedItem, TCollectedItem>
    {
        public ObservableListSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(converter, dataGetter, disposer)
        {
        }

        public ObservableListSynchronizer(IReadOnlyObservableList<TObservedItem> reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference, converter, dataGetter, disposer)
        {
        }
    }

    public class ObservableListSynchronizer<TObservedList, TObservedItem, TCollectedItem> : ObservableListSynchronizer<TObservedList, IList<TCollectedItem>, TObservedItem, TCollectedItem>
        where TObservedList : class, IEnumerable<TObservedItem>, INotifyCollectionChanged
    {
        public ObservableListSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(converter, dataGetter, disposer)
        {
        }

        public ObservableListSynchronizer(TObservedList reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference, converter, dataGetter, disposer)
        {
        }
    }

    public class ObservableListSynchronizer<TObservedList, TCollectedCollection, TObservedItem, TCollectedItem> : ObservableCollectionSynchronizer<TObservedList, TCollectedCollection, TObservedItem, TCollectedItem>
        where TObservedList : class, IEnumerable<TObservedItem>, INotifyCollectionChanged
        where TCollectedCollection : class, IList<TCollectedItem>
    {
        public ObservableListSynchronizer(Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(converter, dataGetter, disposer)
        {
        }

        public ObservableListSynchronizer(TObservedList reference, Func<TObservedItem, TCollectedItem> converter, Func<TCollectedItem, TObservedItem> dataGetter, Action<TCollectedItem> disposer = null)
            : base(reference, converter, dataGetter, disposer)
        {
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
                default:
                {
                    base.OnReferenceCollectionChanged(list, e);
                    return;
                }
            }
        }
    }
}