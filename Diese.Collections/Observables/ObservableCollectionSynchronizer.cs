using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.Base;
using Diese.Collections.Observables.ReadOnly;

namespace Diese.Collections.Observables
{
    public interface ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem>
    {
        TCollectedItem CreateItem(TReferenceItem referenceItem);
        TReferenceItem GetReference(TCollectedItem collectedItem);
        void SubscribeItem(TCollectedItem collectedItem);
        void UnsubscribeItem(TCollectedItem collectedItem);
        void DisposeItem(TCollectedItem collectedItem);
    }

    public class ObservableCollectionSynchronizer<TReferenceItem, TCollectedItem> : ObservableCollectionSynchronizer<IReadOnlyObservableCollection<TReferenceItem>, TReferenceItem, TCollectedItem>
    {
        public ObservableCollectionSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
            : base(configuration)
        {
        }
    }

    public class ObservableCollectionSynchronizer<TReference, TReferenceItem, TCollectedItem> : ObservableCollectionSynchronizer<TReference, ICollection<TCollectedItem>, TReferenceItem, TCollectedItem>
        where TReference : class, IEnumerable<TReferenceItem>, INotifyCollectionChanged
    {
        public ObservableCollectionSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
            : base(configuration)
        {
        }
    }

    public class ObservableCollectionSynchronizer<TReference, TCollection, TReferenceItem, TCollectedItem> : ObservableCollectionSynchronizerBase<TReference, TCollection>
        where TReference : class, IEnumerable<TReferenceItem>, INotifyCollectionChanged
        where TCollection : class, ICollection<TCollectedItem>
    {
        protected readonly ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> Configuration;

        public ObservableCollectionSynchronizer(ICollectionSynchronizerConfiguration<TReferenceItem, TCollectedItem> configuration)
        {
            Configuration = configuration;
        }

        protected override void InitializeCollection(TCollection collection)
        {
            if (Reference != null)
                AddCollectionItems(collection, Reference);
        }

        protected override void ResetCollection(TCollection collection)
        {
            foreach (TCollectedItem item in collection)
                Configuration.UnsubscribeItem(item);

            TCollectedItem[] clearedItems = collection.ToArray();
            collection.Clear();

            foreach (TCollectedItem item in clearedItems)
                Configuration.DisposeItem(item);
        }

        protected override void OnReferenceCollectionChanged(TCollection collection, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    AddCollectionItems(collection, e.NewItems);
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    RemoveCollectionItems(collection, e.OldItems);
                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    RemoveCollectionItems(collection, e.OldItems);
                    AddCollectionItems(collection, e.NewItems);
                    break;
                }
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                {
                    ResetCollection(collection);
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

        private void RemoveCollectionItems(TCollection collection, IEnumerable referenceItems)
        {
            foreach (TCollectedItem itemToRemove in FindCollectionItems(collection, referenceItems))
            {
                Configuration.UnsubscribeItem(itemToRemove);
                collection.Remove(itemToRemove);

                Configuration.DisposeItem(itemToRemove);
            }
        }

        protected IEnumerable<TCollectedItem> FindCollectionItems(TCollection collection, IEnumerable referenceItems)
        {
            return referenceItems
                .Cast<TReferenceItem>()
                .Select(x => collection.First(y => EqualityComparer<TReferenceItem>.Default.Equals(Configuration.GetReference(y), x)));
        }
    }
}