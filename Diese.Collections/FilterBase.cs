using System.Collections;
using System.Collections.Generic;

namespace Diese.Collections
{
    public abstract class FilterBase<T> : IFilter<T>
    {
        public List<T> List { get; } = new List<T>();
        public abstract bool Excluding { get; }

        protected FilterBase()
        {
        }

        protected FilterBase(IEnumerable<T> enumerable)
        {
            List.AddRange(enumerable);
        }

        protected FilterBase(params T[] array)
        {
            List.AddRange(array);
        }

        public abstract bool Filter(T item);
    }
}