using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables.ReadOnly
{
    public class RecomposedReadOnlyObservableList<T> : RecomposedReadOnlyObservableCollection<T>, IReadOnlyObservableList<T>
    {
        private readonly IReadOnlyList<T> _readOnlyList;
        public T this[int index] => _readOnlyList[index];

        public RecomposedReadOnlyObservableList(IReadOnlyList<T> readOnlyList,
            INotifyPropertyChanged notifyPropertyChanged, INotifyCollectionChanged notifyCollectionChanged)
            : base(readOnlyList, notifyPropertyChanged, notifyCollectionChanged)
        {
            _readOnlyList = readOnlyList;
        }
    }
}