using System;
using System.Collections.Generic;

namespace Diese.Collections.Children
{
    public class ChildrenList<TOwner, TChildren> : ChildrenList<TOwner, TChildren, IList<TChildren>>
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
    {
        public ChildrenList(TOwner owner)
            : base(owner, new List<TChildren>())
        {
        }
    }

    public class ChildrenList<TOwner, TChildren, TCollection> : ChildrenCollection<TOwner, TChildren, TCollection>, IList<TChildren>
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
        where TCollection : class, IList<TChildren>
    {
        protected ChildrenList(TOwner owner, TCollection collection)
            : base(owner, collection)
        {
        }

        public TChildren this[int index]
        {
            get => Collection[index];
            set => CheckAndReplace(index, value);
        }

        protected virtual bool CheckAndReplace(int index, TChildren newItem)
        {
            if (newItem == null)
                throw new ArgumentException("Item cannot be null.", nameof(newItem));
            if (Contains(newItem))
                return false;
                
            TChildren oldItem = Collection[index];
            Collection[index] = newItem;

            oldItem.Parent = null;
            newItem.Parent = Owner;
            return true;
        }

        public void Insert(int index, TChildren item) => CheckAndInsert(index, item);

        protected virtual bool CheckAndInsert(int index, TChildren item)
        {
            if (item == null)
                throw new ArgumentException("Item cannot be null.", nameof(item));
            if (Contains(item))
                return false;

            Collection.Insert(index, item);
            item.Parent = Owner;
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            TChildren item = Collection[index];
            Collection.RemoveAt(index);
            item.Parent = null;
        }

        public int IndexOf(TChildren item)
        {
            return item != null ? Collection.IndexOf(item) : -1;
        }
    }
}