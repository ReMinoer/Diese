using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.ReadOnly;

namespace Diese.Collections.Observables
{
    public class ObservableListSynchronizer<TReferenceItem, TCollectedItem> : ObservableListSynchronizer<IReadOnlyObservableList<TReferenceItem>, TReferenceItem, TCollectedItem>
    {
        public ObservableListSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
            : base(configuration)
        {
        }
    }

    public class ObservableListSynchronizer<TReference, TReferenceItem, TCollectedItem> : ObservableListSynchronizer<TReference, IList<TCollectedItem>, TReferenceItem, TCollectedItem>
        where TReference : class, IEnumerable<TReferenceItem>, INotifyCollectionChanged
    {
        public ObservableListSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
            : base(configuration)
        {
        }
    }

    public class ObservableListSynchronizer<TReference, TCollection, TReferenceItem, TCollectedItem> : ObservableCollectionSynchronizer<TReference, TCollection, TReferenceItem, TCollectedItem>
        where TReference : class, IEnumerable<TReferenceItem>, INotifyCollectionChanged
        where TCollection : class, IList<TCollectedItem>
    {
        public ObservableListSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
            : base(configuration)
        {
        }

        protected override void OnReferenceCollectionChanged(TCollection list, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if (e.NewStartingIndex == -1)
                        goto default;

                    InsertCollectionItems(list, e.NewItems, e.NewStartingIndex);
                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    if (e.NewStartingIndex == -1)
                        goto default;

                    ReplaceCollectionItems(list, e.NewItems, e.NewStartingIndex);
                    break;
                }
                case NotifyCollectionChangedAction.Move:
                {
                    if (e.NewStartingIndex == -1)
                        goto default;

                    list.MoveMany(FindCollectionItems(list, e.NewItems), e.NewStartingIndex);
                    break;
                }
                default:
                {
                    base.OnReferenceCollectionChanged(list, e);
                    break;
                }
            }
        }

        private void InsertCollectionItems(TCollection collection, IList referenceItems, int index)
        {
            foreach (TReferenceItem referenceItem in referenceItems.Cast<TReferenceItem>())
            {
                TCollectedItem collectedItem = Configuration.CreateItem(referenceItem);

                collection.Insert(index, collectedItem);
                Configuration.SubscribeItem(collectedItem);

                index++;
            }
        }

        private void ReplaceCollectionItems(TCollection collection, IList referenceItems, int index)
        {
            foreach (TReferenceItem referenceItem in referenceItems.Cast<TReferenceItem>())
            {
                TCollectedItem previousItem = collection[index];

                Configuration.UnsubscribeItem(previousItem);
                collection.RemoveAt(index);
                Configuration.DisposeItem(previousItem);

                TCollectedItem collectedItem = Configuration.CreateItem(referenceItem);

                collection.Insert(index, collectedItem);
                Configuration.SubscribeItem(collectedItem);

                index++;
            }
        }
    }
}