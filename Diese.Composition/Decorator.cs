using System;
using System.Collections.Generic;
using Diese.Composition.Base;
using Diese.Composition.Exceptions;

namespace Diese.Composition
{
    public class Decorator<TAbstract, TParent, TComponent> : ComponentBase<TAbstract, TParent>,
        IDecorator<TAbstract, TParent, TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
        public TComponent Component { get; set; }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public override sealed TAbstract GetComponent(string name, bool includeItself = false)
        {
            if (includeItself && Name == name)
                return this as TAbstract;

            if (includeItself && Component.Name == name)
                return Component;

            return null;
        }

        public override sealed TAbstract GetComponent(Type type, bool includeItself = false)
        {
            if (includeItself && type.IsInstanceOfType(this))
                return this as TAbstract;

            if (includeItself && type.IsInstanceOfType(Component))
                return Component;

            return null;
        }

        public override sealed T GetComponent<T>(bool includeItself = false)
        {
            if (includeItself && this is T)
                return this as T;

            return Component as T;
        }

        public override sealed TAbstract GetComponentInChildren(string name, bool includeItself = false)
        {
            TAbstract component = GetComponent(name, includeItself);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren(name) : null;
        }

        public override sealed TAbstract GetComponentInChildren(Type type, bool includeItself = false)
        {
            TAbstract component = GetComponent(type, includeItself);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren(type) : null;
        }

        public override sealed T GetComponentInChildren<T>(bool includeItself = false)
        {
            var component = GetComponent<T>(includeItself);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren<T>() : null;
        }

        public override sealed List<TAbstract> GetAllComponents(Type type, bool includeItself = false)
        {
            var result = new List<TAbstract>();

            if (includeItself && type.IsInstanceOfType(this))
                result.Add(this as TAbstract);

            if (type.IsInstanceOfType(Component))
                result.Add(Component);

            return result;
        }

        public override sealed List<T> GetAllComponents<T>(bool includeItself = false)
        {
            var result = new List<T>();

            if (includeItself && this is T)
                result.Add(this as T);

            if (Component is T)
                result.Add(Component as T);

            return result;
        }

        public override sealed List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false)
        {
            List<TAbstract> result = GetAllComponents(type, includeItself);

            if (Component != null)
                result.AddRange(Component.GetAllComponentsInChildren(type));

            return result;
        }

        public override sealed List<T> GetAllComponentsInChildren<T>(bool includeItself = false)
        {
            List<T> result = GetAllComponents<T>(includeItself);

            if (Component != null)
                result.AddRange(Component.GetAllComponentsInChildren<T>());

            return result;
        }

        public override sealed bool Contains(TAbstract component)
        {
            return Component.Equals(component);
        }

        public override sealed bool ContainsInChildren(TAbstract component)
        {
            return Contains(component) || Component.ContainsInChildren(component);
        }

        public void Link(TAbstract child)
        {
            if (Component != null)
                Component.Parent = null;

            if (!(child is TComponent))
                throw new InvalidChildException("Component provided is not of type " + typeof(TComponent) + " !");

            Component = (TComponent)child;
            Component.Parent = this as TParent;
        }

        public void Unlink(TAbstract child)
        {
            if (Component != child)
                throw new InvalidChildException("Component provided is not a child of this parent !");

            Component = null;
            child.Parent = null;
        }
    }
}