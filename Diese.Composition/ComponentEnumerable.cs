using System.Collections;
using System.Collections.Generic;

namespace Diese.Composition
{
    public abstract class ComponentEnumerable<TAbstract> : IComponent<TAbstract>, IEnumerable<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
        public abstract string Name { get; set; }
        public abstract IEnumerator<TAbstract> GetEnumerator();

        public T GetComponent<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return this as T;

            foreach (TAbstract component in this)
                if (component is T)
                    return component as T;

            return null;
        }

        public T GetComponentInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            var component = GetComponent<T>(includeItself);
            if (component != null)
                return component;

            foreach (TAbstract child in this)
            {
                var first = child.GetComponentInChildren<T>();
                if (first != null)
                    return first;
            }

            return null;
        }

        public List<T> GetAllComponents<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            var result = new List<T>();

            if (includeItself && this is T)
                result.Add(this as T);

            foreach (TAbstract component in this)
                if (component is T)
                    result.Add(component as T);

            return result;
        }

        public List<T> GetAllComponentsInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            List<T> result = GetAllComponents<T>(includeItself);

            foreach (TAbstract child in this)
                result.AddRange(child.GetAllComponentsInChildren<T>());

            return result;
        }

        public bool ContainsComponent(IComponent<TAbstract> component)
        {
            foreach (TAbstract child in this)
                if (child.Equals(component))
                    return true;

            return false;
        }

        public bool ContainsComponentInChildren(IComponent<TAbstract> component)
        {
            if (ContainsComponent(component))
                return true;

            foreach (TAbstract child in this)
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