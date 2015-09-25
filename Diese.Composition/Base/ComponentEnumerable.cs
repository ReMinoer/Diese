using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Diese.Composition.Exceptions;

namespace Diese.Composition.Base
{
    public abstract class ComponentEnumerable<TAbstract, TParent, TComponent> : ComponentBase<TAbstract, TParent>, IParent<TAbstract, TParent>, IEnumerable<TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
        public override sealed TAbstract GetComponent(string name)
        {
            return this.FirstOrDefault(component => component.Name == name);
        }

        public override sealed TAbstract GetComponent(Type type)
        {
            return this.FirstOrDefault(type.IsInstanceOfType);
        }

        public override sealed T GetComponent<T>()
        {
            return this.OfType<T>().FirstOrDefault();
        }

        public override sealed TAbstract GetComponentInChildren(string name)
        {
            TAbstract component = GetComponent(name);
            if (component != null)
                return component;

            foreach (TComponent child in this)
            {
                component = child.GetComponentInChildren(name);
                if (component != null)
                    return component;
            }

            return null;
        }

        public override sealed TAbstract GetComponentInChildren(Type type)
        {
            TAbstract component = GetComponent(type);
            if (component != null)
                return component;

            foreach (TComponent child in this)
            {
                component = child.GetComponentInChildren(type);
                if (component != null)
                    return component;
            }

            return null;
        }

        public override sealed T GetComponentInChildren<T>()
        {
            var component = GetComponent<T>();
            if (component != null)
                return component;

            foreach (TComponent child in this)
            {
                component = child.GetComponentInChildren<T>();
                if (component != null)
                    return component;
            }

            return null;
        }

        public override sealed IEnumerable<TAbstract> GetAllComponents(Type type)
        {
            return this.Where(type.IsInstanceOfType);
        }

        public override sealed IEnumerable<T> GetAllComponents<T>()
        {
            return this.OfType<T>();
        }

        public override sealed IEnumerable<TAbstract> GetAllComponentsInChildren()
        {
            var result = new List<TAbstract>();
            result.AddRange(this);

            foreach (TComponent child in this)
                result.AddRange(child.GetAllComponentsInChildren());

            return result;
        }

        public override sealed IEnumerable<TAbstract> GetAllComponentsInChildren(Type type)
        {
            List<TAbstract> result = GetAllComponents(type).ToList();

            foreach (TComponent child in this)
                result.AddRange(child.GetAllComponentsInChildren(type));

            return result;
        }

        public override sealed IEnumerable<T> GetAllComponentsInChildren<T>()
        {
            List<T> result = GetAllComponents<T>().ToList();

            foreach (TComponent child in this)
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

        public abstract IEnumerator<TComponent> GetEnumerator();
        protected abstract void Link(TComponent component);
        protected abstract void Unlink(TComponent component);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void IParent<TAbstract, TParent>.Link(TAbstract child)
        {
            var component = child as TComponent;
            if (component == null)
                throw new InvalidChildException("Component provided must be of type " + typeof(TComponent));

            Link(component);
        }

        void IParent<TAbstract, TParent>.Unlink(TAbstract child)
        {
            var component = child as TComponent;
            if (!Contains(component))
                throw new InvalidChildException("Component provided is not linked to this !");

            Unlink(component);
        }
    }
}