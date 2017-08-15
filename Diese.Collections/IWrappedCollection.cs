using System.Collections.Generic;

namespace Diese.Collections
{
    public interface IWrappedCollection<T> : IReadOnlyCollection<T>
    {
        void Add(T item);
    }
}