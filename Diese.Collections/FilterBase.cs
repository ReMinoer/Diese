using System.Collections.Generic;

namespace Diese.Collections
{
    public abstract class FilterBase<T> : IFilter<T>
    {
        public HashSet<T> Items { get; } = new HashSet<T>();
        public abstract bool Excluding { get; }

        protected FilterBase()
        {
        }

        protected FilterBase(IEnumerable<T> enumerable)
        {
            Items.UnionWith(enumerable);
        }

        protected FilterBase(params T[] array)
        {
            Items.UnionWith(array);
        }

        public abstract bool Filter(T item);
    }
}