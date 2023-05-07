using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Diese.Collections.Observables.ReadOnly
{
    public class InlinableReadOnlyObservableList<T> : InlinableReadOnlyObservableCollection<T>, IReadOnlyObservableList<T>
    {
        public T this[int index] => ObservableCollection[index];

        public InlinableReadOnlyObservableList(IReadOnlyObservableList<T> list, Func<T, IReadOnlyObservableCollection<T>> inlining)
            : base(list, inlining)
        {
        }

        protected override void UpdateObservableCollection(ObservableCollection<T> observableCollection, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    observableCollection.InsertMany(e.NewStartingIndex, e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    observableCollection.ReplaceRange(e.OldStartingIndex, e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move: 
                    observableCollection.MoveMany(e.OldItems, e.NewStartingIndex);
                    break;
                default:
                    base.UpdateObservableCollection(observableCollection, e);
                    break;
            }
        }
    }
}