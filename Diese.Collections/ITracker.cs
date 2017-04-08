using System.Collections.Generic;

namespace Diese.Collections
{
    public interface ITracker<T> : IEnumerable<T>
        where T : class
    {
        int Count { get; }
        void Register(T item);
        bool Unregister(T item);
        void Clear();
        void ClearDisposed();
        bool Contains(T item);
    }
}