using System.Collections.Generic;
using System.Collections.Specialized;

namespace Diese.Collections
{
    public class WrappedCollection<T> : ReadOnlyCollection<T>, IWrappedCollection<T>, INotifyCollectionChanged
    {
        public WrappedCollection(ICollection<T> collection)
            : base(collection)
        {
        }

        void IWrappedCollection<T>.Add(T item)
        {
            Collection.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}