using System.Collections.Generic;

namespace Diese.Composition
{
    public abstract class Component<TAbstract> : IComponent<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
        public virtual string Name { get; set; }

        public T GetComponent<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return this as T;
            return null;
        }

        public T GetComponentInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            return GetComponent<T>(includeItself);
        }

        public List<T> GetAllComponents<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return new List<T> {this as T};

            return new List<T>();
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