using System.Collections.Generic;

namespace Diese.Collections.Observables
{
    public interface IObservableList<T> : IObservableCollection<T>, IList<T>
    {
    }
}