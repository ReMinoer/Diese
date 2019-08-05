using System.Collections;
using System.Collections.Generic;
using Diese.Collections.Observables.ReadOnly.Base;

namespace Diese.Collections.Observables.ReadOnly
{
    public class EnumerableReadOnlyObservableCollection : EnumerableReadOnlyObservableCollectionBase<IEnumerable, object>
    {
        public EnumerableReadOnlyObservableCollection(IEnumerable enumerable)
            : base(enumerable)
        {
        }

        public override IEnumerator<object> GetEnumerator() => new Enumerator(Enumerable.GetEnumerator());

        public class Enumerator : IEnumerator<object>
        {
            private readonly IEnumerator _enumerator;
            public Enumerator(IEnumerator enumerator) => _enumerator = enumerator;

            public object Current => _enumerator.Current;
            object IEnumerator.Current => Current;
            public bool MoveNext() => _enumerator.MoveNext();
            public void Reset() => _enumerator.Reset();
            public void Dispose() {}
        }
    }

    public class EnumerableReadOnlyObservableCollection<T> : EnumerableReadOnlyObservableCollectionBase<IEnumerable<T>, T>
    {
        public EnumerableReadOnlyObservableCollection(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public override IEnumerator<T> GetEnumerator() => Enumerable.GetEnumerator();
    }
}