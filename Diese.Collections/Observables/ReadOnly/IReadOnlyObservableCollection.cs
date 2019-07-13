using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Diese.Collections.Observables.ReadOnly
{
    public interface IReadOnlyObservableCollection<out T> : IReadOnlyCollection<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
    }
}