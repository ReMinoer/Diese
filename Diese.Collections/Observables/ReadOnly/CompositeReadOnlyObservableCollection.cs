using System;
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
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    return CollectionChangedEventArgs.AddRange(e.NewItems);
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    return CollectionChangedEventArgs.RemoveRange(e.OldItems);
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    return CollectionChangedEventArgs.ReplaceRange(e.OldItems, e.NewItems);
                }
                case NotifyCollectionChangedAction.Move:
                {
                    return null;
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    if (sender.Count == Count)
                        return CollectionChangedEventArgs.Clear();
                    
                    return CollectionChangedEventArgs.RemoveRange(sender.ToList());
                }
                default:
                    throw new NotSupportedException();
            }
        }
    }
}