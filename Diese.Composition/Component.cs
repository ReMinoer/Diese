using System;
using System.Collections.Generic;

namespace Diese.Composition
{
    public class Component<TAbstract> : IComponent<TAbstract>
        where TAbstract : class, IComponent<TAbstract>
    {
        public string Name { get; set; }

        public TAbstract GetComponent(string name, bool includeItself = false)
        {
            if (includeItself && Name == name)
                return this as TAbstract;
            return null;
        }

        public TAbstract GetComponent(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return this as TAbstract;
            return null;
        }

        public T GetComponent<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return this as T;
            return null;
        }

        public TAbstract GetComponentInChildren(string name, bool includeItself = false)
        {
            return GetComponent(name, includeItself);
        }

        public TAbstract GetComponentInChildren(Type type, bool includeItself = false)
        {
            return GetComponent(type, includeItself);
        }

        public T GetComponentInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            return GetComponent<T>(includeItself);
        }

        public List<TAbstract> GetAllComponents(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return new List<TAbstract> {this as TAbstract};

            return new List<TAbstract>();
        }

        public List<T> GetAllComponents<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return new List<T> {this as T};

            return new List<T>();
        }

        public List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false)
        {
            return GetAllComponents(type, includeItself);
        }

        public List<T> GetAllComponentsInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            return GetAllComponents<T>(includeItself);
        }

        public bool ContainsComponent(IComponent<TAbstract> component)
        {
            return false;
        }

        public bool ContainsComponentInChildren(IComponent<TAbstract> component)
        {
            return false;
        }
    }
}