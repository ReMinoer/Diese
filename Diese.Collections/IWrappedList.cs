using System.Collections.Generic;

namespace Diese.Collections
{
    public interface IWrappedList<T> : IWrappedCollection<T>, IReadOnlyList<T>
    {
    }
}