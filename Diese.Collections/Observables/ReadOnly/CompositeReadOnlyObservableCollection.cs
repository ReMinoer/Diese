using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.ReadOnly.Base;

namespace Diese.Collections.Observables.ReadOnly
{
    public class CompositeReadOnlyObservableCollection<T> : CompositeReadOnlyObservableCollectionBase<T, IReadOnlyObservableCollection<T>>
    {
        public CompositeReadOnlyObservableCollection(params IReadOnlyObservableCollection<T>[] observableCollections)
            : base(observableCollections)
        {
        }

        protected override NotifyCollectionChangedEventArgs BuildCompositeEventArgs(IReadOnlyObservableCollection<T> sender, NotifyCollectionChangedEventArgs e)
        {
            int GetSenderStartIndex() => ObservableCollections.TakeWhile(x => x != sender).Sum(x => x.Count);
            int GetNewItemsIndex() => GetSenderStartIndex() + (e.NewStartingIndex >= 0 ? e.NewStartingIndex : sender.Count);
            int GetOldItemsIndex() => GetSenderStartIndex() + (e.OldStartingIndex >= 0 ? e.OldStartingIndex : sender.IndexOf(x => EqualityComparer<T>.Default.Equals(x, (T)e.OldItems[0])));

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    return CollectionChangedEventArgs.InsertRange(e.NewItems, GetNewItemsIndex());
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    return CollectionChangedEventArgs.RemoveRange(e.OldItems, GetOldItemsIndex());
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    return CollectionChangedEventArgs.ReplaceRange(e.OldItems, e.NewItems, GetOldItemsIndex());
                }
                case NotifyCollectionChangedAction.Move:
                {
                    return null;
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    if (sender.Count == Count)
                        return CollectionChangedEventArgs.Clear();
                    
                    return CollectionChangedEventArgs.RemoveRange(sender.ToList(), GetSenderStartIndex());
                }
                default:
                    throw new NotSupportedException();
            }
        }
    }
}