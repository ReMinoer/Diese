using System.Collections.Generic;

namespace Diese.Collections.ReadOnly
{
    public interface IWrappedList<T> : IWrappedCollection<T>, IReadOnlyList<T>
    {
    }
}