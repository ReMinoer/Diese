using System;
using System.Collections.Generic;
using Diese.Composition.Base;

namespace Diese.Composition
{
    public class Component<TAbstract> : ComponentBase<TAbstract>
        where TAbstract : class, IComponent<TAbstract>
    {
        public override string Name { get; set; }

        public override sealed TAbstract GetComponent(string name, bool includeItself = false)
        {
            if (includeItself && Name == name)
                return this as TAbstract;
            return null;
        }

        public override sealed TAbstract GetComponent(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return this as TAbstract;
            return null;
        }

        public override sealed T GetComponent<T>(bool includeItself = false)
        {
            if (includeItself && this is T)
                return this as T;
            return null;
        }

        public override sealed TAbstract GetComponentInChildren(string name, bool includeItself = false)
        {
            return GetComponent(name, includeItself);
        }

        public override sealed TAbstract GetComponentInChildren(Type type, bool includeItself = false)
        {
            return GetComponent(type, includeItself);
        }

        public override sealed T GetComponentInChildren<T>(bool includeItself = false)
        {
            return GetComponent<T>(includeItself);
        }

        public override sealed List<TAbstract> GetAllComponents(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return new List<TAbstract> {this as TAbstract};

            return new List<TAbstract>();
        }

        public override sealed List<T> GetAllComponents<T>(bool includeItself = false)
        {
            if (includeItself && this is T)
                return new List<T> {this as T};

            return new List<T>();
        }

        public override sealed List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false)
        {
            return GetAllComponents(type, includeItself);
        }

        public override sealed List<T> GetAllComponentsInChildren<T>(bool includeItself = false)
        {
            return GetAllComponents<T>(includeItself);
        }

        public override sealed bool ContainsComponent(IComponent<TAbstract> component)
        {
            return false;
        }

        public override sealed bool ContainsComponentInChildren(IComponent<TAbstract> component)
        {
            return false;
        }
    }
}