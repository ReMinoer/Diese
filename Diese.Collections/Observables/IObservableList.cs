using System.Collections.Generic;

namespace Diese.Collections.Observables
{
    public interface IObservableList : IObservableCollection
    {
        void RemoveAt(int index);
        void Move(int oldIndex, int newIndex);
    }

    public interface IObservableList<T> : IObservableList, IObservableCollection<T>, IList<T>
    {
        new void RemoveAt(int index);
    }
}