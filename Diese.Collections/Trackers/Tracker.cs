using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections.Trackers
{
    public class Tracker<T> : ITracker<T>
        where T : class
    {
        private readonly List<WeakReference<T>> _list;

        bool ICollection<WeakReference<T>>.IsReadOnly
        {
            get { return false; }
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public Tracker()
        {
            _list = new List<WeakReference<T>>();
        }

        public virtual T this[int index]
        {
            get
            {
                T obj;
                _list[index].TryGetTarget(out obj);
                return obj;
            }
            set
            {
                _list[index].SetTarget(value);
            }
        }

        public virtual void Register(T item)
        {
            _list.Add(new WeakReference<T>(item));
        }

        public void RegisterAt(int index, T item)
        {
            _list.Insert(index, new WeakReference<T>(item));
        }

        public virtual bool Unregister(T item)
        {
            return _list.Remove(new WeakReference<T>(item));
        }

        public void UnregisterAt(int index)
        {
            _list.RemoveAt(index);
        }

        public virtual void Clear()
        {
            _list.Clear();
        }

        public void ClearDisposed()
        {
            foreach (WeakReference<T> weakReference in _list.Where(x =>
            {
                T obj;
                return !x.TryGetTarget(out obj);
            }).ToList())
                _list.Remove(weakReference);
        }

        public bool Contains(T item)
        {
            return _list.Any(x =>
            {
                T obj;
                x.TryGetTarget(out obj);
                return obj == item;
            });
        }

        public int IndexOf(T item)
        {
            return _list.FindIndex(x =>
            {
                T obj;
                x.TryGetTarget(out obj);
                return obj == item;
            });
        }

        public IReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyTracker<T>(this);
        }

        WeakReference<T> IList<WeakReference<T>>.this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        void ICollection<WeakReference<T>>.Add(WeakReference<T> item)
        {
            _list.Add(item);
        }

        bool ICollection<WeakReference<T>>.Remove(WeakReference<T> item)
        {
            return _list.Remove(item);
        }

        bool ICollection<WeakReference<T>>.Contains(WeakReference<T> item)
        {
            return _list.Contains(item);
        }

        void ICollection<WeakReference<T>>.CopyTo(WeakReference<T>[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        int IList<WeakReference<T>>.IndexOf(WeakReference<T> item)
        {
            return _list.IndexOf(item);
        }

        void IList<WeakReference<T>>.Insert(int index, WeakReference<T> item)
        {
            _list.Insert(index, item);
        }

        void IList<WeakReference<T>>.RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.Select(x =>
            {
                T obj;
                x.TryGetTarget(out obj);
                return obj;
            }).GetEnumerator();
        }

        IEnumerator<WeakReference<T>> IEnumerable<WeakReference<T>>.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}