using System;
using System.Collections.Generic;

namespace Diese.Composition
{
    public class Composite<TAbstract, TParent>
        : ComponentEnumerable<TAbstract, TParent, TAbstract>, IComposite<TAbstract, TParent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, IParent<TAbstract, TParent>
    {
        protected readonly List<TAbstract> Components;

        public int Count
        {
            get { return Components.Count; }
        }

        public override sealed bool IsReadOnly
        {
            get { return false; }
        }

        protected Composite()
        {
            Components = new List<TAbstract>();
        }

        public virtual void Add(TAbstract item)
        {
            if (Equals(item))
                throw new InvalidOperationException("Item can't be a child of itself.");
            if (ContainsAmongParents(item))
                throw new InvalidOperationException(
                    "Item can't be a child of this because it is already among its parents.");

            item.Parent = this as TParent;
            Components.Add(item);
        }

        public virtual void Clear()
        {
            Components.Clear();
        }

        public void CopyTo(TAbstract[] array, int arrayIndex)
        {
            Components.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(TAbstract item)
        {
            bool valid = Components.Remove(item);

            if (valid)
                item.Parent = null;

            return valid;
        }

        public override IEnumerator<TAbstract> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        public override sealed void Link(TAbstract child)
        {
            Add(child);
        }

        public override sealed void Unlink(TAbstract child)
        {
            Remove(child);
        }
    }
}