using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public abstract class OrderedTrackerBase<T> : IOrderedTracker<T>, IList<T>, IList
        where T : class
    {
        private readonly List<T> _list;
        public int Count => _list.Count;

        protected OrderedTrackerBase()
        {
            _list = new List<T>();
        }

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public virtual bool Register(T item)
        {
            if (!CanRegister(item))
                return false;

            _list.Add(item);
            Subscribe(item);
            return true;
        }

        public bool Register(int index, T item)
        {
            if (!CanRegister(item))
                return false;

            _list.Insert(index, item);
            Subscribe(item);
            return true;
        }

        public virtual bool Unregister(T item)
        {
            if (!_list.Remove(item))
                return false;

            Unsubscribe(item);
            return true;
        }

        public bool UnregisterAt(int index)
        {
            T item = this[index];
            Unsubscribe(item);
            _list.RemoveAt(index);
            return true;
        }

        public bool UnregisterRange(int index, int count)
        {
            foreach (T item in _list.Skip(index).Take(count))
                Unsubscribe(item);

            _list.RemoveRange(index, count);
            return true;
        }

        public virtual void Clear()
        {
            foreach (T item in _list)
                Unsubscribe(item);

            _list.Clear();
        }

        public bool Contains(T item) => _list.Contains(item);
        public int IndexOf(T item) => _list.IndexOf(item);

        protected abstract bool CanRegister(T item);
        protected abstract void Subscribe(T item);
        protected abstract void Unsubscribe(T item);

        bool ICollection<T>.IsReadOnly => false;
        void ICollection<T>.Add(T item) => Register(item);
        bool ICollection<T>.Remove(T item) => Unregister(item);
        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        void IList<T>.Insert(int index, T item) => _list.Insert(index, item);
        void IList<T>.RemoveAt(int index) => _list.RemoveAt(index);

        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;
        bool IList.IsFixedSize => false;
        bool IList.IsReadOnly => false;

        object IList.this[int index] { get => _list[index]; set => _list[index] = (T)value; }
        int IList.Add(object value)
        {
            _list.Add((T)value);
            return _list.Count - 1;
        }
        void IList.Insert(int index, object value) => _list.Insert(index, (T)value);
        void IList.Remove(object value) => _list.Remove((T)value);
        void IList.RemoveAt(int index) => _list.RemoveAt(index);
        bool IList.Contains(object value) => _list.Contains((T)value);
        int IList.IndexOf(object value) => _list.IndexOf((T)value);
        void ICollection.CopyTo(Array array, int index) => _list.CopyTo((T[])array, index);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}