using System.Collections.Generic;
using System.Collections.Specialized;

namespace Diese.Collections
{
    public interface IReadOnlyObservableCollection<out T> : IReadOnlyCollection<T>, INotifyCollectionChanged
    {
    }
}