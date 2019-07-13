using System.Collections.Generic;

namespace Diese.Collections.Observables.ReadOnly
{
    public interface IReadOnlyObservableList<out T> : IReadOnlyObservableCollection<T>, IReadOnlyList<T>
    {
    }
}