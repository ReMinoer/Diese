using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.Base;
using Diese.Collections.Observables.ReadOnly;

namespace Diese.Collections.Observables
{
    public class ObservableObjectListSynchronizer<TReferenceItem, TCollectedItem> : ObservableObjectListSynchronizer<IReadOnlyObservableList<TReferenceItem>, TReferenceItem, TCollectedItem>
    {
        public ObservableObjectListSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
            : base(configuration)
        {
        }
    }

    public class ObservableObjectListSynchronizer<TReference, TReferenceItem, TCollectedItem> : ObservableObjectListSynchronizer<TReference, IList, TReferenceItem, TCollectedItem>
        where TReference : class, IEnumerable<TReferenceItem>, INotifyCollectionChanged
    {
        public ObservableObjectListSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
            : base(configuration)
        {
        }
    }

    public class ObservableObjectListSynchronizer<TReference, TCollection, TReferenceItem, TCollectedItem> : ObservableCollectionSynchronizerBase<TReference, TCollection>
        where TReference : class, IEnumerable<TReferenceItem>, INotifyCollectionChanged
        where TCollection : class, IList
    {
        protected readonly ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> Configuration;

        public ObservableObjectListSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
        {
            Configuration = configuration;
        }

        protected override void InitializeCollection(TCollection list)
        {
            if (Reference != null)
                AddCollectionItems(list, Reference);
        }

        protected override void ResetCollection(TCollection list)
        {
            foreach (TCollectedItem item in list)
                Configuration.UnsubscribeItem(item);

            TCollectedItem[] clearedItems = list.Cast<TCollectedItem>().ToArray();
            list.Clear();

            foreach (TCollectedItem item in clearedItems)
                Configuration.DisposeItem(item);
        }

        protected override void OnReferenceCollectionChanged(TCollection list, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if (e.NewStartingIndex == -1)
                        AddCollectionItems(list, e.NewItems);

                    InsertCollectionItems(list, e.NewItems, e.NewStartingIndex);
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    RemoveCollectionItems(list, e.OldItems);
                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    if (e.NewStartingIndex == -1)
                    {
                        RemoveCollectionItems(list, e.OldItems);
                        AddCollectionItems(list, e.NewItems);
                    }
                        
                    ReplaceCollectionItems(list, e.NewItems, e.NewStartingIndex);
                    break;
                }
                case NotifyCollectionChangedAction.Move:
                {
                    if (e.NewStartingIndex == -1)
                        break;

                    list.MoveMany(FindCollectionItems(list, e.NewItems), e.NewStartingIndex);
                    break;
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    ResetCollection(list);
                    break;
                }
                default:
                    throw new NotSupportedException();
            }
        }

        private void AddCollectionItems(TCollection collection, IEnumerable referenceItems)
        {
            foreach (TReferenceItem referenceItem in referenceItems.Cast<TReferenceItem>())
            {
                TCollectedItem collectedItem = Configuration.CreateItem(referenceItem);

                collection.Add(collectedItem);
                Configuration.SubscribeItem(collectedItem);
            }
        }

        private void InsertCollectionItems(TCollection collection, IEnumerable referenceItems, int index)
        {
            foreach (TReferenceItem referenceItem in referenceItems.Cast<TReferenceItem>())
            {
                TCollectedItem collectedItem = Configuration.CreateItem(referenceItem);

                collection.Insert(index, collectedItem);
                Configuration.SubscribeItem(collectedItem);

                index++;
            }
        }

        private void RemoveCollectionItems(TCollection collection, IEnumerable referenceItems)
        {
            foreach (TCollectedItem itemToRemove in FindCollectionItems(collection, referenceItems))
            {
                Configuration.UnsubscribeItem(itemToRemove);
                collection.Remove(itemToRemove);

                Configuration.DisposeItem(itemToRemove);
            }
        }

        private void ReplaceCollectionItems(TCollection collection, IEnumerable referenceItems, int index)
        {
            foreach (TReferenceItem referenceItem in referenceItems.Cast<TReferenceItem>())
            {
                var previousItem = (TCollectedItem)collection[index];

                Configuration.UnsubscribeItem(previousItem);
                collection.RemoveAt(index);
                Configuration.DisposeItem(previousItem);

                TCollectedItem collectedItem = Configuration.CreateItem(referenceItem);

                collection.Insert(index, collectedItem);
                Configuration.SubscribeItem(collectedItem);

                index++;
            }
        }

        private IEnumerable<TCollectedItem> FindCollectionItems(TCollection collection, IEnumerable referenceItems)
        {
            return referenceItems
                .Cast<TReferenceItem>()
                .Select(x => collection.Cast<TCollectedItem>().First(y => EqualityComparer<TReferenceItem>.Default.Equals(Configuration.GetReference(y), x)));
        }
    }
}