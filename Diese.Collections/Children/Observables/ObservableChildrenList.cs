using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Diese.Collections.Observables;
using Diese.Collections.Utils;

namespace Diese.Collections.Children.Observables
{
    public class ObservableChildrenList<TOwner, TChildren> : ObservableChildrenList<TOwner, TChildren, IObservableList<TChildren>>
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
    {
        public ObservableChildrenList(TOwner owner)
            : base(owner, new ObservableList<TChildren>())
        {
        }
    }

    public class ObservableChildrenList<TOwner, TChildren, TCollection> : ChildrenList<TOwner, TChildren, TCollection>, IObservableList<TChildren>, IDisposable
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
        where TCollection : class, IObservableList<TChildren>
    {
        private readonly CollectionChangeDelayer<TChildren> _collectionChangeDelayer;
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected ObservableChildrenList(TOwner owner, TCollection collection)
            : base(owner, collection)
        {
            _collectionChangeDelayer = new CollectionChangeDelayer<TChildren>(collection);
        }

        protected override bool CheckAndReplace(int index, TChildren newItem)
        {
            if (!base.CheckAndReplace(index, newItem))
                return false;
            
            NotifyCollectionChange();
            return true;
        }

        protected override bool CheckAndAdd(TChildren item)
        {
            if (!base.CheckAndAdd(item))
                return false;
            
            NotifyCollectionChange();
            return true;
        }

        protected override bool CheckAndInsert(int index, TChildren item)
        {
            if (!base.CheckAndInsert(index, item))
                return false;
            
            NotifyCollectionChange();
            return true;
        }

        public override bool Remove(TChildren item)
        {
            if (!base.Remove(item))
                return false;
            
            NotifyCollectionChange();
            return true;
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            NotifyCollectionChange();
        }

        public override void Clear()
        {
            if (Count == 0)
                return;

            base.Clear();
            NotifyCollectionChange();
        }

        protected void NotifyCollectionChange()
        {
            if (_collectionChangeDelayer.GetCountChange())
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));

            NotifyCollectionChangedEventArgs lastChange = _collectionChangeDelayer.GetLastChange();
            CollectionChanged?.Invoke(this, lastChange);
        }

        public void Dispose()
        {
            _collectionChangeDelayer.Dispose();
        }
    }
}