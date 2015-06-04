using System;
using System.Collections.Generic;

namespace Diese.Composition
{
    public class Decorator<TAbstract, TComponent> : IDecorator<TAbstract, TComponent>
        where TAbstract : class, IComponent<TAbstract>
        where TComponent : TAbstract
    {
        public string Name { get; set; }
        public TComponent Component { get; set; }

        public TAbstract GetComponent(string name, bool includeItself = false)
        {
            if (includeItself && Name == name)
                return this as TAbstract;

            if (includeItself && Component.Name == name)
                return Component;

            return null;
        }

        public TAbstract GetComponent(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return this as TAbstract;

            if (includeItself && type.IsInstanceOfType(Component))
                return Component;

            return null;
        }

        public T GetComponent<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return this as T;

            return Component as T;
        }

        public TAbstract GetComponentInChildren(string name, bool includeItself = false)
        {
            TAbstract component = GetComponent(name, includeItself);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren(name) : null;
        }

        public TAbstract GetComponentInChildren(Type type, bool includeItself = false)
        {
            TAbstract component = GetComponent(type, includeItself);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren(type) : null;
        }

        public T GetComponentInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            var component = GetComponent<T>(includeItself);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren<T>() : null;
        }

        public List<TAbstract> GetAllComponents(Type type, bool includeItself = false)
        {
            var result = new List<TAbstract>();

            if (includeItself && type.IsInstanceOfType(this))
                result.Add(this as TAbstract);

            if (type.IsInstanceOfType(Component))
                result.Add(Component);

            return result;
        }

        public List<T> GetAllComponents<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            var result = new List<T>();

            if (includeItself && this is T)
                result.Add(this as T);

            if (Component is T)
                result.Add(Component as T);

            return result;
        }

        public List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false)
        {
            List<TAbstract> result = GetAllComponents(type, includeItself);

            if (Component != null)
                result.AddRange(Component.GetAllComponentsInChildren(type));

            return result;
        }

        public List<T> GetAllComponentsInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            List<T> result = GetAllComponents<T>(includeItself);

            if (Component != null)
                result.AddRange(Component.GetAllComponentsInChildren<T>());

            return result;
        }

        public bool ContainsComponent(IComponent<TAbstract> component)
        {
            return Component.Equals(component);
        }

        public bool ContainsComponentInChildren(IComponent<TAbstract> component)
        {
            if (ContainsComponent(component))
                return true;

            return Component.ContainsComponentInChildren(component);
        }
    }
}