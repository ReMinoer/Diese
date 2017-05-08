using System.Collections.Generic;

namespace Diese.Collections
{
    public interface IFilter<T>
    {
        List<T> List { get; }
        bool Excluding { get; }
        bool Filter(T item);
    }
}