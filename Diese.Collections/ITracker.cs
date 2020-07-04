using System.Collections.Generic;

namespace Diese.Collections
{
    public interface ITracker<T> : IEnumerable<T>
        where T : class
    {
        int Count { get; }
        bool Register(T item);
        bool Unregister(T item);
        void Clear();
        bool Contains(T item);
    }

    public interface IOrderedTracker<T> : ITracker<T>
        where T : class
    {
        T this[int index] { get; set; }
        bool Register(int index, T item);
        bool UnregisterAt(int index);
        bool UnregisterRange(int index, int count);
        int IndexOf(T item);
    }
}