using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables.ReadOnly
{
    public class RecomposedReadOnlyObservableCollection<T> : IReadOnlyObservableCollection<T>
    {
        private readonly IReadOnlyCollection<T> _readOnlyCollection;
        private readonly INotifyPropertyChanged _notifyPropertyChanged;
        private readonly INotifyCollectionChanged _notifyCollectionChanged;

        public int Count => _readOnlyCollection.Count;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (_notifyPropertyChanged is null)
                    return;

                _notifyPropertyChanged.PropertyChanged += value;
            }
            remove
            {
                if (_notifyPropertyChanged is null)
                    return;

                _notifyPropertyChanged.PropertyChanged -= value;
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                if (_notifyCollectionChanged is null)
                    return;

                _notifyCollectionChanged.CollectionChanged += value;
            }
            remove
            {
                if (_notifyCollectionChanged is null)
                    return;

                _notifyCollectionChanged.CollectionChanged -= value;
            }
        }

        public RecomposedReadOnlyObservableCollection(IReadOnlyCollection<T> readOnlyCollection,
            INotifyPropertyChanged notifyPropertyChanged, INotifyCollectionChanged notifyCollectionChanged)
        {
            _readOnlyCollection = readOnlyCollection;
            _notifyPropertyChanged = notifyPropertyChanged;
            _notifyCollectionChanged = notifyCollectionChanged;
        }

        public IEnumerator<T> GetEnumerator() => _readOnlyCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}