using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables
{
    public class ObservableList<T> : IObservableList<T>, IList
    {
        private readonly IList<T> _list;
        private readonly IList _nonGenericList;

        public int Count => _list.Count;
        bool ICollection<T>.IsReadOnly => false;

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableList()
        {
            var list = new List<T>();
            _list = list;
            _nonGenericList = list;
        }

        public ObservableList(IEnumerable<T> items)
        {
            var list = new List<T>(items);
            _list = list;
            _nonGenericList = list;
        }

        public ObservableList(IList<T> listImplementation)
        {
            _list = listImplementation;
            _nonGenericList = listImplementation as IList;
        }
        
        private void NotifyCountChange() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));

        public T this[int index]
        {
            get => _list[index];
            set
            {
                if (CollectionChanged != null)
                {
                    T oldItem = _list[index];
                    _list[index] = value;
                    CollectionChanged.Invoke(this, CollectionChangedEventArgs.Replace(oldItem, value, index));
                    return;
                }

                _list[index] = value;
            }
        }

        public void Add(T item)
        {
            _list.Add(item);

            NotifyCountChange();
            CollectionChanged?.Invoke(this, CollectionChangedEventArgs.Add(item, Count));
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);

            NotifyCountChange();
            CollectionChanged?.Invoke(this, CollectionChangedEventArgs.Insert(item, index));
        }

        public bool Remove(T item)
        {
            if (CollectionChanged == null)
                return _list.Remove(item);

            int index = _list.IndexOf(item);
            if (index == -1)
                return false;
            
            _list.RemoveAt(index);

            NotifyCountChange();
            CollectionChanged.Invoke(this, CollectionChangedEventArgs.Remove(item, index));
            return true;
        }

        public void RemoveAt(int index)
        {
            if (CollectionChanged == null)
                _list.RemoveAt(index);
            else
            {
                T item = _list[index];
                _list.RemoveAt(index);

                NotifyCountChange();
                CollectionChanged.Invoke(this, CollectionChangedEventArgs.Remove(item, index));
            }
        }

        public void Clear()
        {
            _list.Clear();

            NotifyCountChange();
            CollectionChanged?.Invoke(this, CollectionChangedEventArgs.Clear());
        }

        public bool Contains(T item) => _list.Contains(item);
        public int IndexOf(T item) => _list.IndexOf(item);
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list).GetEnumerator();
        
        #region IList implementation

        bool IList.IsFixedSize => _nonGenericList?.IsFixedSize ?? false;
        bool IList.IsReadOnly => _list.IsReadOnly;
        bool ICollection.IsSynchronized => _nonGenericList?.IsSynchronized ?? false;

        private object _syncRoot;
        object ICollection.SyncRoot
        {
            get
            {
                if (_nonGenericList?.SyncRoot != null)
                    return _nonGenericList.SyncRoot;
                
                if( _syncRoot == null)
                    System.Threading.Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }

        object IList.this[int index]
        {
            get => _list[index];
            set => _list[index] = (T)value;
        }

        int IList.Add(object value)
        {
            Add((T)value);
            return Count - 1;
        }

        void IList.Insert(int index, object value) => Insert(index, (T)value);
        void IList.Remove(object value) => Remove((T)value);
        bool IList.Contains(object value) => Contains((T)value);
        int IList.IndexOf(object value) => IndexOf((T)value);

        void ICollection.CopyTo(Array array, int index)
        {
            int i = index;
            foreach (T item in _list)
            {
                if (i >= array.Length)
                    return;

                array.SetValue(item, i);
                i++;
            }
        }

        #endregion
    }
}