using System;
using System.Collections.Generic;
using System.Linq;
using Diese.Composition.Base;
using Diese.Composition.Exceptions;

namespace Diese.Composition
{
    public class Decorator<TAbstract, TParent, TComponent> : ComponentBase<TAbstract, TParent>, IDecorator<TAbstract, TParent, TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
        private TComponent _component;

        public TComponent Component
        {
            get { return _component; }
            set
            {
                if (_component != null)
                    throw new InvalidChildException("You must unlink a decorator before assign a new component !");

                if (value != null)
                {
                    if (Equals(value))
                        throw new InvalidOperationException("Item can't be a child of itself.");
                    if (ContainsAmongParents(value))
                        throw new InvalidOperationException("Item can't be a child of this because it already exist among its parents.");
                }

                _component = value;

                if (value != null)
                    value.Parent = this as TParent;
            }
        }

        public override sealed TAbstract GetComponent(string name)
        {
            return Component != null && Component.Name == name ? Component : null;
        }

        public override sealed TAbstract GetComponent(Type type)
        {
            return Component != null && type.IsInstanceOfType(Component) ? Component : null;
        }

        public override sealed T GetComponent<T>()
        {
            return Component as T;
        }

        public override sealed TAbstract GetComponentInChildren(string name)
        {
            TAbstract component = GetComponent(name);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren(name) : null;
        }

        public override sealed TAbstract GetComponentInChildren(Type type)
        {
            TAbstract component = GetComponent(type);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren(type) : null;
        }

        public override sealed T GetComponentInChildren<T>()
        {
            var component = GetComponent<T>();
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren<T>() : null;
        }

        public override sealed IEnumerable<TAbstract> GetAllComponents(Type type)
        {
            var result = new List<TAbstract>();

            if (Component != null && type.IsInstanceOfType(Component))
                result.Add(Component);

            return result;
        }

        public override sealed IEnumerable<T> GetAllComponents<T>()
        {
            var result = new List<T>();

            if (Component is T)
                result.Add(Component as T);

            return result;
        }

        public override sealed IEnumerable<TAbstract> GetAllComponentsInChildren()
        {
            var result = new List<TAbstract>
            {
                Component
            };

            if (Component != null)
                result.AddRange(Component.GetAllComponentsInChildren());

            return result;
        }

        public override sealed IEnumerable<TAbstract> GetAllComponentsInChildren(Type type)
        {
            List<TAbstract> result = GetAllComponents(type).ToList();

            if (Component != null)
                result.AddRange(Component.GetAllComponentsInChildren(type));

            return result;
        }

        public override sealed IEnumerable<T> GetAllComponentsInChildren<T>()
        {
            List<T> result = GetAllComponents<T>().ToList();

            if (Component != null)
                result.AddRange(Component.GetAllComponentsInChildren<T>());

            return result;
        }

        public override sealed bool Contains(TAbstract component)
        {
            return Component != null && Component.Equals(component);
        }

        public override sealed bool ContainsInChildren(TAbstract component)
        {
            return Contains(component) || Component.ContainsInChildren(component);
        }

        public TComponent Unlink()
        {
            TComponent component = Component;
            Component = null;
            return component;
        }

        void IParent<TAbstract, TParent>.Link(TAbstract child)
        {
            var component = child as TComponent;
            if (component == null)
                throw new InvalidChildException("Component provided must be of type " + typeof(TComponent) + " !");

            Component = component;
        }

        void IParent<TAbstract, TParent>.Unlink(TAbstract child)
        {
            if (Component != child)
                throw new InvalidChildException("Component provided is not linked !");

            Component = null;
        }
    }
}