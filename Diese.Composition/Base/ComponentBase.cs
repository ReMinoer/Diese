using System;
using System.Collections.Generic;

namespace Diese.Composition.Base
{
    public abstract class ComponentBase<TAbstract, TParent> : IComponent<TAbstract, TParent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
        private TParent _parent;
        public string Name { get; set; }

        public TParent Parent
        {
            get { return _parent; }
            set
            {
                if (_parent.Equals(value))
                    return;

                _parent.Unlink(this as TAbstract);

                _parent = value;
                _parent.Link(this as TAbstract);
            }
        }

        public abstract TAbstract GetComponent(string name, bool includeItself = false);
        public abstract TAbstract GetComponent(Type type, bool includeItself = false);
        public abstract T GetComponent<T>(bool includeItself = false) where T : class, TAbstract;
        public abstract TAbstract GetComponentInChildren(string name, bool includeItself = false);
        public abstract TAbstract GetComponentInChildren(Type type, bool includeItself = false);
        public abstract T GetComponentInChildren<T>(bool includeItself = false) where T : class, TAbstract;
        public abstract List<TAbstract> GetAllComponents(Type type, bool includeItself = false);
        public abstract List<T> GetAllComponents<T>(bool includeItself = false) where T : class, TAbstract;
        public abstract List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false);
        public abstract List<T> GetAllComponentsInChildren<T>(bool includeItself = false) where T : class, TAbstract;
        public abstract bool ContainsComponent(TAbstract component);
        public abstract bool ContainsComponentInChildren(TAbstract component);

        public TAbstract GetComponentAmongParents(string name)
        {
            if (Parent == null)
                return null;

            if (Parent.Name == name)
                return Parent as TAbstract;

            return Parent.GetComponentAmongParents(name);
        }

        public TAbstract GetComponentAmongParents(Type type)
        {
            if (Parent == null)
                return null;

            if (type.IsInstanceOfType(Parent))
                return Parent as TAbstract;

            return Parent.GetComponentAmongParents(type);
        }

        public T GetComponentAmongParents<T>() where T : class, TAbstract
        {
            var parent = Parent as T;
            return parent ?? Parent.GetComponentAmongParents<T>();
        }

        public bool ContainsComponentAmongParents(TAbstract component)
        {
            if (Parent == null)
                return false;

            return Parent.Equals(component) || Parent.ContainsComponentAmongParents(component);
        }
    }
}