using System;
using System.Collections;
using System.Collections.Generic;

namespace Diese.Composition
{
    public abstract class ComponentEnumerable<TAbstract, TInput> : IComponent<TAbstract>, IEnumerable<TInput>
        where TAbstract : class, IComponent<TAbstract>
        where TInput : TAbstract
    {
        public abstract string Name { get; set; }
        public abstract IEnumerator<TInput> GetEnumerator();

        public TAbstract GetComponent(string name, bool includeItself = false)
        {
            if (Name == name)
                return this as TAbstract;

            foreach (TInput component in this)
                if (component.Name == name)
                    return component;

            return null;
        }

        public TAbstract GetComponent(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return this as TAbstract;

            foreach (TInput component in this)
                if (type.IsInstanceOfType(component))
                    return component;

            return null;
        }

        public T GetComponent<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return this as T;

            foreach (TInput component in this)
                if (component is T)
                    return component as T;

            return null;
        }

        public TAbstract GetComponentInChildren(string name, bool includeItself = false)
        {
            TAbstract component = GetComponent(name, includeItself);
            if (component != null)
                return component;

            foreach (TInput child in this)
            {
                TAbstract first = child.GetComponentInChildren(name);
                if (first != null)
                    return first;
            }

            return null;
        }

        public TAbstract GetComponentInChildren(Type type, bool includeItself = false)
        {
            TAbstract component = GetComponent(type, includeItself);
            if (component != null)
                return component;

            foreach (TInput child in this)
            {
                TAbstract first = child.GetComponentInChildren(type);
                if (first != null)
                    return first;
            }

            return null;
        }

        public T GetComponentInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            var component = GetComponent<T>(includeItself);
            if (component != null)
                return component;

            foreach (TInput child in this)
            {
                var first = child.GetComponentInChildren<T>();
                if (first != null)
                    return first;
            }

            return null;
        }

        public List<TAbstract> GetAllComponents(Type type, bool includeItself = false)
        {
            var result = new List<TAbstract>();

            if (includeItself && type.IsInstanceOfType(this))
                result.Add(this as TAbstract);

            foreach (TInput component in this)
                if (type.IsInstanceOfType(component))
                    result.Add(component);

            return result;
        }

        public List<T> GetAllComponents<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            var result = new List<T>();

            if (includeItself && this is T)
                result.Add(this as T);

            foreach (TInput component in this)
                if (component is T)
                    result.Add(component as T);

            return result;
        }

        public List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false)
        {
            List<TAbstract> result = GetAllComponents(type, includeItself);

            foreach (TInput child in this)
                result.AddRange(child.GetAllComponentsInChildren(type));

            return result;
        }

        public List<T> GetAllComponentsInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            List<T> result = GetAllComponents<T>(includeItself);

            foreach (TInput child in this)
                result.AddRange(child.GetAllComponentsInChildren<T>());

            return result;
        }

        public bool ContainsComponent(IComponent<TAbstract> component)
        {
            foreach (TInput child in this)
                if (child.Equals(component))
                    return true;

            return false;
        }

        public bool ContainsComponentInChildren(IComponent<TAbstract> component)
        {
            if (ContainsComponent(component))
                return true;

            foreach (TInput child in this)
                if (child.ContainsComponentInChildren(component))
                    return true;

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}