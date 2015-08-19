using System;

namespace Diese.Composition
{
    public class OrderedComposite<TAbstract, TParent, TComponent> : Composite<TAbstract, TParent, TComponent>, IOrderedComposite<TAbstract, TParent, TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
        public virtual TComponent this[int index]
        {
            get { return _components[index]; }
            set
            {
                if (value != null)
                {
                    if (Equals(value))
                        throw new InvalidOperationException("Item can't be a child of itself.");
                    if (ContainsAmongParents(value))
                        throw new InvalidOperationException("Item can't be a child of this because it already exist among its parents.");

                    if (!Contains(value))
                        _components.Add(value);
                }

                _components[index] = value;
            }
        }

        public int IndexOf(TComponent item)
        {
            return _components.IndexOf(item);
        }

        public virtual void Insert(int index, TComponent item)
        {
            if (Equals(item))
                throw new InvalidOperationException("Item can't be a child of itself.");
            if (ContainsAmongParents(item))
                throw new InvalidOperationException("Item can't be a child of this because it is already among its parents.");

            if (!Contains(item))
                _components.Add(item);

            _components.Insert(index, item);

            if (item != null)
                item.Parent = this as TParent;
        }

        public virtual void RemoveAt(int index)
        {
            _components.RemoveAt(index);
            _components[index].Parent = null;
        }
    }
}