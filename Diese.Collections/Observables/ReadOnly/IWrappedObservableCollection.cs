using Diese.Collections.ReadOnly;

namespace Diese.Collections.Observables.ReadOnly
{
    public interface IWrappedObservableCollection<T> : IWrappedCollection<T>, IReadOnlyObservableCollection<T>
    {
    }
}