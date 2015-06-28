using System;

namespace Diese.Composition
{
    public class OrderedComposite<TAbstract, TParent> : Composite<TAbstract, TParent>,
        IOrderedComposite<TAbstract, TParent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, IParent<TAbstract, TParent>
    {
        public int IndexOf(TAbstract item)
        {
            return Components.IndexOf(item);
        }

        public virtual void Insert(int index, TAbstract item)
        {
            if (Equals(item))
                throw new InvalidOperationException("Item can't be a child of itself.");
            if (ContainsAmongParents(item))
                throw new InvalidOperationException(
                    "Item can't be a child of this because it is already among its parents.");

            Components.Insert(index, item);
            item.Parent = this as TParent;
        }

        public virtual void RemoveAt(int index)
        {
            Components[index].Parent = null;
            Components.RemoveAt(index);
        }

        public virtual TAbstract this[int index]
        {
            get { return Components[index]; }
            set { Components[index] = value; }
        }
    }
}