using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class ExcludingFilter<T> : FilterBase<T>
    {
        static public ExcludingFilter<T> None { get; } = new ExcludingFilter<T>();

        public override sealed bool Excluding => true;

        public ExcludingFilter()
        {
        }

        public ExcludingFilter(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public ExcludingFilter(params T[] array)
            : base(array)
        {
        }

        public override sealed bool Filter(T item)
        {
            return !Items.Contains(item);
        }

        public IncludingFilter<T> ToIncluding(IEnumerable<T> data)
        {
            return new IncludingFilter<T>(data.Where(x => !Filter(x)));
        }
    }
}