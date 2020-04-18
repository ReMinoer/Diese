using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    public class CompositeReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly IEnumerable<IReadOnlyCollection<T>> _observableCollections;
        public int Count => _observableCollections.Sum(x => x.Count);

        public CompositeReadOnlyCollection(IEnumerable<IReadOnlyCollection<T>> observableCollections)
        {
            _observableCollections = observableCollections;
        }

        public IEnumerator<T> GetEnumerator() => _observableCollections.SelectMany(x => x).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}