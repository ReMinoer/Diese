using System;
using System.Collections.Generic;

namespace Diese.Composition.Base
{
    public abstract class ComponentBase<TAbstract, TParent> : IComponent<TAbstract, TParent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
    {
        private TParent _parent;
        public string Name { get; set; }

        public TParent Parent
        {
            get { return _parent; }
            set
            {
                var baseComponent = this as TAbstract;

                if (value != _parent)
                {
                    if (_parent != null)
                        _parent.Unlink(baseComponent);
                
                    _parent = value;
                }

                if (value != null && !_parent.Contains(baseComponent))
                    _parent.Link(baseComponent);
            }
        }

        protected ComponentBase()
        {
            Name = GetType().Name;
        }

        public abstract TAbstract GetComponent(string name);
        public abstract TAbstract GetComponent(Type type);
        public abstract T GetComponent<T>() where T : class, TAbstract;
        public abstract TAbstract GetComponentInChildren(string name);
        public abstract TAbstract GetComponentInChildren(Type type);
        public abstract T GetComponentInChildren<T>() where T : class, TAbstract;
        public abstract IEnumerable<TAbstract> GetAllComponents(Type type);
        public abstract IEnumerable<T> GetAllComponents<T>() where T : class, TAbstract;
        public abstract IEnumerable<TAbstract> GetAllComponentsInChildren(Type type);
        public abstract IEnumerable<T> GetAllComponentsInChildren<T>() where T : class, TAbstract;
        public abstract bool Contains(TAbstract component);
        public abstract bool ContainsInChildren(TAbstract component);

        public TAbstract GetComponentAmongParents(string name)
        {
            if (Parent == null)
                return null;

            return Parent.Name == name ? Parent : Parent.GetComponentAmongParents(name);
        }

        public TAbstract GetComponentAmongParents(Type type)
        {
            if (Parent == null)
                return null;

            return type.IsInstanceOfType(Parent) ? Parent : Parent.GetComponentAmongParents(type);
        }

        public T GetComponentAmongParents<T>() where T : class, TAbstract
        {
            var parent = Parent as T;
            return parent ?? Parent.GetComponentAmongParents<T>();
        }

        public bool ContainsAmongParents(TAbstract component)
        {
            if (Parent == null)
                return false;

            return Parent.Equals(component) || Parent.ContainsAmongParents(component);
        }
    }
}