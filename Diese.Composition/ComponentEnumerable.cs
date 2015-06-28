using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Diese.Composition.Base;

namespace Diese.Composition
{
    public abstract class ComponentEnumerable<TAbstract, TParent, TInput> : ComponentBase<TAbstract, TParent>,
        IComponentEnumerable<TAbstract, TParent, TInput>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
        where TInput : TAbstract
    {
        public abstract bool IsReadOnly { get; }
        public abstract IEnumerator<TInput> GetEnumerator();
        public abstract void Link(TAbstract child);
        public abstract void Unlink(TAbstract child);

        public override sealed TAbstract GetComponent(string name, bool includeItself = false)
        {
            if (Name == name)
                return this as TAbstract;

            foreach (TInput component in this)
                if (component.Name == name)
                    return component;

            return null;
        }

        public override sealed TAbstract GetComponent(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return this as TAbstract;

            foreach (TInput component in this)
                if (type.IsInstanceOfType(component))
                    return component;

            return null;
        }

        public override sealed T GetComponent<T>(bool includeItself = false)
        {
            if (includeItself && this is T)
                return this as T;

            return this.OfType<T>().FirstOrDefault();
        }

        public override sealed TAbstract GetComponentInChildren(string name, bool includeItself = false)
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

        public override sealed TAbstract GetComponentInChildren(Type type, bool includeItself = false)
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

        public override sealed T GetComponentInChildren<T>(bool includeItself = false)
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

        public override sealed List<TAbstract> GetAllComponents(Type type, bool includeItself = false)
        {
            var result = new List<TAbstract>();

            if (includeItself && type.IsInstanceOfType(this))
                result.Add(this as TAbstract);

            foreach (TInput component in this)
                if (type.IsInstanceOfType(component))
                    result.Add(component);

            return result;
        }

        public override sealed List<T> GetAllComponents<T>(bool includeItself = false)
        {
            var result = new List<T>();

            if (includeItself && this is T)
                result.Add(this as T);

            result.AddRange(this.OfType<T>());

            return result;
        }

        public override sealed List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false)
        {
            List<TAbstract> result = GetAllComponents(type, includeItself);

            foreach (TInput child in this)
                result.AddRange(child.GetAllComponentsInChildren(type));

            return result;
        }

        public override sealed List<T> GetAllComponentsInChildren<T>(bool includeItself = false)
        {
            List<T> result = GetAllComponents<T>(includeItself);

            foreach (TInput child in this)
                result.AddRange(child.GetAllComponentsInChildren<T>());

            return result;
        }

        public override sealed bool Contains(TAbstract component)
        {
            return this.Any(child => child.Equals(component));
        }

        public override sealed bool ContainsInChildren(TAbstract component)
        {
            return Contains(component) || this.Any(child => child.ContainsInChildren(component));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}