using System;
using System.Collections;
using System.Collections.Generic;

namespace Diese
{
    public class DisposableCollection : ICollection<IDisposable>, IDisposable
    {
        private readonly ICollection<IDisposable> _collection = new List<IDisposable>();
        public int Count => _collection.Count;
        bool ICollection<IDisposable>.IsReadOnly => _collection.IsReadOnly;

        public DisposableCollection()
        {
        }

        public DisposableCollection(IEnumerable<IDisposable> items)
        {
            AddRange(items);
        }

        public DisposableCollection(params IDisposable[] items)
        {
            AddRange(items);
        }

        public void Add(IDisposable item)
        {
            _collection.Add(item);
        }

        public void AddRange(IEnumerable<IDisposable> items)
        {
            foreach (IDisposable item in items)
                _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(IDisposable item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(IDisposable item)
        {
            return _collection.Remove(item);
        }

        public IEnumerator<IDisposable> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_collection).GetEnumerator();
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _collection)
                disposable.Dispose();
        }
    }
}