using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class Tracker<T> : ITracker<T>, ICollection<T>
        where T : class
    {
        private readonly List<WeakReference<T>> _list;
        public int Count => _list.Count;
        bool ICollection<T>.IsReadOnly => false;

        public Tracker()
        {
            _list = new List<WeakReference<T>>();
        }

        public virtual void Register(T item)
        {
            _list.Add(new WeakReference<T>(item));
        }

        void ICollection<T>.Add(T item)
        {
            Register(item);
        }

        public virtual bool Unregister(T item)
        {
            return _list.Remove(x => ReferenceEquals(x, item));
        }

        bool ICollection<T>.Remove(T item)
        {
            return Unregister(item);
        }

        public virtual void Clear()
        {
            _list.Clear();
        }

        public void ClearDisposed()
        {
            _list.RemoveAll(x => !x.TryGetTarget(out T _));
        }

        public bool Contains(T item)
        {
            return _list.Any(x => ReferenceEquals(x, item));
        }

        private bool ReferenceEquals(WeakReference<T> x, T item)
        {
            x.TryGetTarget(out T obj);
            return obj == item;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _list.Where(x => x.TryGetTarget(out T _)).ToArray().CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.Select(x =>
            {
                x.TryGetTarget(out T obj);
                return obj;
            }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}