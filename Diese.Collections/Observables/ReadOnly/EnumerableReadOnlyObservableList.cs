using System.Collections;
using System.Collections.Generic;

namespace Diese.Collections.Observables.ReadOnly
{
    public class EnumerableReadOnlyObservableList : EnumerableReadOnlyObservableCollection, IReadOnlyObservableList<object>
    {
        public object this[int index] => Enumerable.ElementAt(index);

        public EnumerableReadOnlyObservableList(IEnumerable enumerable)
            : base(enumerable)
        {
        }
    }

    public class EnumerableReadOnlyObservableList<T> : EnumerableReadOnlyObservableCollection<T>, IReadOnlyObservableList<T>
    {
        public T this[int index] => System.Linq.Enumerable.ElementAt(Enumerable, index);

        public EnumerableReadOnlyObservableList(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }
    }
}