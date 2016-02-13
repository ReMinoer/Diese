using System;
using System.Collections.Generic;

namespace Diese.Collections
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface ITracker<T> : IList<WeakReference<T>>, IEnumerable<T>
        where T : class
    {
        new T this[int index] { get; set; }
        void Register(T item);
        void RegisterAt(int index, T item);
        bool Unregister(T item);
        void UnregisterAt(int index);
        void ClearDisposed();
        bool Contains(T item);
        int IndexOf(T item);
        IReadOnlyCollection<T> AsReadOnly();
    }
}