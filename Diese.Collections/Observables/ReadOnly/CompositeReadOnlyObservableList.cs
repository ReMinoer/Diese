using System;
using System.Collections.Specialized;
using System.Linq;
using Diese.Collections.Observables.ReadOnly.Base;

namespace Diese.Collections.Observables.ReadOnly
{
    public class CompositeReadOnlyObservableList<T> : CompositeReadOnlyObservableCollectionBase<T, IReadOnlyObservableList<T>>, IReadOnlyObservableList<T>
    {
        public CompositeReadOnlyObservableList(params IReadOnlyObservableList<T>[] observableCollections)
            : base(observableCollections)
        {
        }

        public T this[int index]
        {
            get
            {
                foreach (IReadOnlyObservableList<T> observableList in ObservableCollections)
                {
                    if (index < observableList.Count)
                        return observableList[index];

                    index -= observableList.Count;
                }

                throw new IndexOutOfRangeException();
            }
        }

        protected override NotifyCollectionChangedEventArgs BuildCompositeEventArgs(IReadOnlyObservableList<T> sender, NotifyCollectionChangedEventArgs e)
        {
            int GetSenderStartIndex() => ObservableCollections.TakeWhile(x => x != sender).Sum(x => x.Count);
            int GetNewItemsIndex() => GetSenderStartIndex() + (e.NewStartingIndex >= 0 ? e.NewStartingIndex : throw new InvalidOperationException());
            int GetOldItemsIndex() => GetSenderStartIndex() + (e.OldStartingIndex >= 0 ? e.OldStartingIndex : throw new InvalidOperationException());

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
                    return CollectionChangedEventArgs.MoveRange(e.OldItems, GetOldItemsIndex(), GetNewItemsIndex());
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