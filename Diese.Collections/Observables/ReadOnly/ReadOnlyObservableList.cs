using System;

namespace Diese.Collections.Observables.ReadOnly
{
    public class ReadOnlyObservableList<T> : ReadOnlyObservableCollection<T>, IReadOnlyObservableList<T>
    {
        private readonly IObservableList<T> _observableList;

        public T this[int index] => _observableList[index];

        public ReadOnlyObservableList(IObservableList<T> observableList)
            : base(observableList)
        {
            _observableList = observableList;
        }
    }
}