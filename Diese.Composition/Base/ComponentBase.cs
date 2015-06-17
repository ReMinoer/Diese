using System;
using System.Collections.Generic;

namespace Diese.Composition.Base
{
    public abstract class ComponentBase<TAbstract> : IComponent<TAbstract>
        where TAbstract : class, IComponent<TAbstract>
    {
        private IParent<TAbstract> _parent;

        public IParent<TAbstract> Parent
        {
            get { return _parent; }
            set
            {
                if (_parent == value)
                    return;

                _parent.Unlink(this as TAbstract);

                _parent = value;
                _parent.Link(this as TAbstract);
            }
        }

        public TAbstract GetComponentInParents(string name)
        {
            if (Parent == null)
                return null;

            if (Parent.Name == name)
                return Parent as TAbstract;

            return Parent.GetComponentInParents(name);
        }

        public TAbstract GetComponentInParents(Type type)
        {
            if (Parent == null)
                return null;

            if (type.IsInstanceOfType(Parent))
                return Parent as TAbstract;

            return Parent.GetComponentInParents(type);
        }

        public T GetComponentInParents<T>() where T : class, TAbstract
        {
            var parent = Parent as T;
            return parent ?? Parent.GetComponentInParents<T>();
        }

        public abstract string Name { get; set; }
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
        public abstract bool ContainsComponent(IComponent<TAbstract> component);
        public abstract bool ContainsComponentInChildren(IComponent<TAbstract> component);
    }
}