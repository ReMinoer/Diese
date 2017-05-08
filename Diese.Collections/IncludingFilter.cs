using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class IncludingFilter<T> : FilterBase<T>
    {
        static public IncludingFilter<T> None { get; } = new IncludingFilter<T>();

        public override sealed bool Excluding => false;

        public IncludingFilter()
        {
        }

        public IncludingFilter(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public IncludingFilter(params T[] array)
            : base(array)
        {
        }

        public override sealed bool Filter(T item)
        {
            return List.Contains(item);
        }

        public ExcludingFilter<T> ToExcluding(IEnumerable<T> data)
        {
            var excludingFilter = new ExcludingFilter<T>();
            excludingFilter.List.AddRange(data.Where(x => !Filter(x)));
            return excludingFilter;
        }
    }
}