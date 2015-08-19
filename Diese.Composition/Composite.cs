using System;
using System.Collections.Generic;
using Diese.Composition.Base;
using Diese.Composition.Exceptions;

namespace Diese.Composition
{
    public class Composite<TAbstract, TParent, TComponent> : ComponentEnumerable<TAbstract, TParent, TComponent>, IComposite<TAbstract, TParent, TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
        protected readonly List<TComponent> _components;

        public int Count
        {
            get { return _components.Count; }
        }

        protected Composite()
        {
            _components = new List<TComponent>();
        }

        public virtual void Add(TComponent item)
        {
            if (Equals(item))
                throw new InvalidOperationException("Item can't be a child of itself.");
            if (ContainsAmongParents(item))
                throw new InvalidOperationException("Item can't be a child of this because it already exist among its parents.");

            if (!Contains(item))
                _components.Add(item);

            item.Parent = this as TParent;
        }

        public virtual void Remove(TComponent item)
        {
            if (!_components.Contains(item))
                throw new InvalidChildException("Component provided is not linked !");

            _components.Remove(item);
            item.Parent = null;
        }

        public virtual void Clear()
        {
            for (int i = Count; i >= 0; i--)
                Remove(_components[0]);
        }

        public override IEnumerator<TComponent> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        protected override sealed void Link(TComponent component)
        {
            Add(component);
        }

        protected override sealed void Unlink(TComponent component)
        {
            Remove(component);
        }
    }
}