using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables
{
    public interface IObservableCollection : INotifyPropertyChanged, INotifyCollectionChanged
    {
        int Count { get; }
        void Clear();
    }

    public interface IObservableCollection<T> : IObservableCollection, ICollection<T>
    {
        new int Count { get; }
        new void Clear();
    }
}