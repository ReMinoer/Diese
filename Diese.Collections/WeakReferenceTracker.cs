using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class WeakReferenceTracker<T> : ITracker<T>, ICollection<T>, IList
        where T : class
    {
        private readonly List<WeakReference<T>> _list;
        public int Count => _list.Count;

        public WeakReferenceTracker()
        {
            _list = new List<WeakReference<T>>();
        }

        public virtual bool Register(T item)
        {
            var weakReference = new WeakReference<T>(item);
            if (!weakReference.TryGetTarget(out _))
                return false;

            _list.Add(weakReference);
            return true;
        }

        public virtual bool Unregister(T item) => _list.Remove(x => ReferenceEquals(x, item));
        public virtual void Clear() => _list.Clear();
        public void Refresh() => _list.RemoveAll(x => !x.TryGetTarget(out T _));
        public bool Contains(T item) => _list.Any(x => ReferenceEquals(x, item));

        static private bool ReferenceEquals(WeakReference<T> x, T item)
        {
            x.TryGetTarget(out T obj);
            return obj == item;
        }

        bool ICollection<T>.IsReadOnly => false;
        void ICollection<T>.Add(T item) => Register(item);
        bool ICollection<T>.Remove(T item) => Unregister(item);
        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _list.Where(x => x.TryGetTarget(out T _)).ToArray().CopyTo(array, arrayIndex);
        }

        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;
        bool IList.IsFixedSize => false;
        bool IList.IsReadOnly => false;

        object IList.this[int index] { get => _list[index]; set => _list[index] = new WeakReference<T>((T)value); }
        int IList.Add(object value)
        {
            _list.Add(new WeakReference<T>((T)value));
            return _list.Count - 1;
        }
        void IList.Insert(int index, object value) => _list.Insert(index, new WeakReference<T>((T)value));
        void IList.Remove(object value) => _list.RemoveAt(_list.FindIndex(x => ReferenceEquals(x, value)));
        void IList.RemoveAt(int index) => _list.RemoveAt(index);
        bool IList.Contains(object value) => _list.Any(x => ReferenceEquals(x, value));
        int IList.IndexOf(object value) => _list.FindIndex(x => ReferenceEquals(x, value));
        void ICollection.CopyTo(Array array, int index)
        {
            _list.Where(x => x.TryGetTarget(out T _)).ToArray().CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.Select(x =>
            {
                x.TryGetTarget(out T obj);
                return obj;
            }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}