using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Diese.Collections.Observables;
using Diese.Collections.Utils;

namespace Diese.Collections.Children.Observables
{
    public class ObservableChildrenCollection<TOwner, TChildren> : ObservableChildrenCollection<TOwner, TChildren, IObservableCollection<TChildren>>
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
    {
        public ObservableChildrenCollection(TOwner owner)
            : base(owner, new ObservableCollection<TChildren>())
        {
        }
    }

    public class ObservableChildrenCollection<TOwner, TChildren, TCollection> : ChildrenCollection<TOwner, TChildren, TCollection>, IObservableCollection<TChildren>, IDisposable
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
        where TCollection : class, IObservableCollection<TChildren>
    {
        private readonly CollectionChangeDelayer<TChildren> _collectionChangeDelayer;
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected ObservableChildrenCollection(TOwner owner, TCollection collection)
            : base(owner, collection)
        {
            _collectionChangeDelayer = new CollectionChangeDelayer<TChildren>(collection);
        }

        protected override bool CheckAndAdd(TChildren item)
        {
            if (!base.CheckAndAdd(item))
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

            CollectionChanged?.Invoke(this, _collectionChangeDelayer.GetLastChange());
        }

        public void Dispose()
        {
            _collectionChangeDelayer.Dispose();
        }
    }
}