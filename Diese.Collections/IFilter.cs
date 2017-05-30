using System.Collections.Generic;

namespace Diese.Collections
{
    public interface IFilter<T>
    {
        HashSet<T> Items { get; }
        bool Excluding { get; }
        bool Filter(T item);
    }
}