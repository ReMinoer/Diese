using System.Collections.Generic;

namespace Diese.Collections
{
    public class WrappedList<T> : ReadOnlyList<T>, IWrappedList<T>
    {
        public WrappedList(IList<T> collection)
            : base(collection)
        {
        }

        void IWrappedCollection<T>.Add(T item)
        {
            List.Add(item);
        }
    }
}