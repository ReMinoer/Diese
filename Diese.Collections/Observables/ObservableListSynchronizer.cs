using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.Base;
using Diese.Collections.Observables.ReadOnly;

namespace Diese.Collections.Observables
{
    public class ObservableListSynchronizer<TObservedItem, TCollectedItem> : ObservableListSynchronizer<IReadOnlyObservableList<TObservedItem>, TObservedItem, TCollectedItem>
    {
        public ObservableListSynchronizer(Func<TObservedItem, TCollectedItem> converter)
            : base(converter)
        {
        }

        public ObservableListSynchronizer(IReadOnlyObservableList<TObservedItem> reference, Func<TObservedItem, TCollectedItem> converter)
            : base(reference, converter)
        {
        }
    }

    public class ObservableListSynchronizer<TObservedList, TObservedItem, TCollectedItem> : ObservableCollectionSynchronizerBase<TObservedList, IList<TCollectedItem>>
        where TObservedList : class, IEnumerable<TObservedItem>, INotifyCollectionChanged
    {
        private readonly Func<TObservedItem, TCollectedItem> _converter;

        public ObservableListSynchronizer(Func<TObservedItem, TCollectedItem> converter)
        {
            _converter = converter;
        }

        public ObservableListSynchronizer(TObservedList reference, Func<TObservedItem, TCollectedItem> converter)
            : base(reference)
        {
            _converter = converter;
        }

        protected override void InitializeCollection(IList<TCollectedItem> list)
        {
            if (Reference != null)
                list.AddMany(Reference.Select(_converter));
        }

        protected override void ResetCollection(IList<TCollectedItem> list)
        {
            list.Clear();
        }

        protected override void OnReferenceCollectionChanged(IList<TCollectedItem> list, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    list.InsertMany(e.NewStartingIndex, e.NewItems.Cast<TObservedItem>().Select(_converter));
                    return;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    list.RemoveMany(e.OldItems.Cast<TObservedItem>().Select(_converter));
                    return;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    list.ReplaceRange(e.OldStartingIndex, e.NewStartingIndex, e.NewItems.Cast<TObservedItem>().Select(_converter));
                    return;
                }
                case NotifyCollectionChangedAction.Move:
                {
                    list.MoveMany(e.NewItems.Cast<TObservedItem>().Select(_converter), e.NewStartingIndex);
                    return;
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    list.Clear();
                    return;
                }
            }
        }
    }
}