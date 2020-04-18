using System.Collections;
using System.Collections.Specialized;

namespace Diese.Collections.Observables
{
    static public class CollectionChangedEventArgs
    {
        static public NotifyCollectionChangedEventArgs Add(object item)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item);

        static public NotifyCollectionChangedEventArgs Add(object item, int newCount)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, newCount - 1);

        static public NotifyCollectionChangedEventArgs AddRange(IList items)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items);

        static public NotifyCollectionChangedEventArgs AddRange(IList items, int newCount)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, newCount - items.Count);

        static public NotifyCollectionChangedEventArgs Insert(object item, int index)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index);

        static public NotifyCollectionChangedEventArgs InsertRange(IList items, int index)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, index);

        static public NotifyCollectionChangedEventArgs Replace(object oldItem, object newItem)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem);

        static public NotifyCollectionChangedEventArgs Replace(object oldItem, object newItem, int index)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, index);

        static public NotifyCollectionChangedEventArgs ReplaceRange(IList oldItems, IList newItems)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItems, newItems);

        static public NotifyCollectionChangedEventArgs ReplaceRange(IList oldItems, IList newItems, int index)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, oldItems, newItems, index);

        static public NotifyCollectionChangedEventArgs Move(IList items, int oldIndex, int newIndex)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, items, oldIndex, newIndex);

        static public NotifyCollectionChangedEventArgs MoveRange(IList items, int oldIndex, int newIndex)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, items, oldIndex, newIndex);

        static public NotifyCollectionChangedEventArgs Remove(object item)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item);

        static public NotifyCollectionChangedEventArgs Remove(object item, int index)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index);

        static public NotifyCollectionChangedEventArgs RemoveRange(IList items)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items);

        static public NotifyCollectionChangedEventArgs RemoveRange(IList items, int index)
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items, index);

        static public NotifyCollectionChangedEventArgs Clear()
            => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
    }
}