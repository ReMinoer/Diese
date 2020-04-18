using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections.Children
{
    public class ChildrenCollection<TOwner, TChildren> : ChildrenCollection<TOwner, TChildren, ICollection<TChildren>>
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
    {
        public ChildrenCollection(TOwner owner)
            : base(owner, new List<TChildren>())
        {
        }
    }

    public class ChildrenCollection<TOwner, TChildren, TCollection> : ICollection<TChildren>
        where TOwner : class
        where TChildren : class, IParentable<TOwner>
        where TCollection : class, ICollection<TChildren>
    {
        protected readonly TCollection Collection;
        public TOwner Owner { get; }

        public int Count => Collection.Count;
        public bool IsReadOnly => Collection.IsReadOnly;

        protected ChildrenCollection(TOwner owner, TCollection collection)
        {
            Owner = owner;
            Collection = collection;
        }

        public void Add(TChildren item) => CheckAndAdd(item);

        protected virtual bool CheckAndAdd(TChildren item)
        {
            if (item == null)
                throw new ArgumentException("Item cannot be null.", nameof(item));
            if (Contains(item))
                return false;

            Collection.Add(item);
            item.Parent = Owner;
            return true;
        }

        public virtual bool Remove(TChildren item)
        {
            if (item == null)
                throw new ArgumentException("Item cannot be null", nameof(item));

            if (!Collection.Remove(item))
                return false;
            
            item.Parent = null;
            return true;
        }

        public virtual void Clear()
        {
            TChildren[] children = Collection.ToArray();
            Collection.Clear();

            foreach (TChildren child in children)
                child.Parent = null;
        }

        public bool Contains(TChildren item)
        {
            if (item == null)
                return false;

            return Collection.Contains(item);
        }

        void ICollection<TChildren>.CopyTo(TChildren[] array, int arrayIndex) => Collection.CopyTo(array, arrayIndex);
        
        private IEnumerable Enumerable => Collection;
        public IEnumerator<TChildren> GetEnumerator() => Collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Enumerable.GetEnumerator();
    }
}