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
            int senderIndex = ObservableCollections.TakeWhile(x => x != sender).Sum(x => x.Count);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    return CollectionChangedEventArgs.InsertRange(e.NewItems, senderIndex + e.NewStartingIndex);
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    return CollectionChangedEventArgs.RemoveRange(e.OldItems, senderIndex + e.OldStartingIndex);
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    return CollectionChangedEventArgs.ReplaceRange(e.OldItems, e.NewItems, senderIndex + e.OldStartingIndex);
                }
                case NotifyCollectionChangedAction.Move:
                {
                    return CollectionChangedEventArgs.MoveRange(e.OldItems, senderIndex + e.OldStartingIndex, senderIndex + e.NewStartingIndex);
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    if (sender.Count == Count)
                        return CollectionChangedEventArgs.Clear();
                    
                    return CollectionChangedEventArgs.RemoveRange(sender.ToList(), senderIndex);
                }
                default:
                    throw new NotSupportedException();
            }
        }
    }
}