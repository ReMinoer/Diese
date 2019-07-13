using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables
{
    public interface IObservableCollection<T> : ICollection<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
    }
}