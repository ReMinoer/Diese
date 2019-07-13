using System.Collections.Generic;

namespace Diese.Collections.ReadOnly
{
    public interface IWrappedCollection<T> : IReadOnlyCollection<T>
    {
        void Add(T item);
    }
}