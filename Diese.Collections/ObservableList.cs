using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Diese.Collections
{
    public class ObservableList<T> : IList<T>, INotifyCollectionChanged
    {
        private readonly List<T> _list = new List<T>();

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public T this[int index]
        {
            get => _list[index];
            set
            {
                if (CollectionChanged != null)
                {
                    T oldItem = _list[index];
                    _list[index] = value;
                    CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
                    return;
                }

                _list[index] = value;
            }
        }

        public void Add(T item)
        {
            _list.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Count - 1));
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (CollectionChanged != null)
            {
                T[] newItems = items.ToArray();
                _list.AddRange(newItems);
                CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems, Count - newItems.Length));
                return;
            }

            _list.AddRange(items);
        }

        public void InsertRange(int index, IEnumerable<T> items)
        {
            if (CollectionChanged != null)
            {
                T[] newItems = items.ToArray();
                _list.InsertRange(index, newItems);
                CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems, index));
                return;
            }

            _list.InsertRange(index, items);
        }

        public bool Remove(T item)
        {
            if (CollectionChanged == null)
                return _list.Remove(item);

            int index = _list.IndexOf(item);
            bool result = _list.Remove(item);
            CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            return result;
        }

        public void RemoveAt(int index)
        {
            if (CollectionChanged != null)
            {
                T item = this[index];
                _list.RemoveAt(index);
                CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
                return;
            }

            _list.RemoveAt(index);
        }

        public void Clear()
        {
            _list.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_list).GetEnumerator();
        }
    }
}