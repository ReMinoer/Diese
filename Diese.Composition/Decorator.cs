using System.Collections.Generic;

namespace Diese.Composition
{
    public class Decorator<TAbstract, TComponent> : IDecorator<TAbstract, TComponent>
        where TAbstract : IComponent<TAbstract>
        where TComponent : TAbstract
    {
        public string Name { get; set; }
        public TComponent Component { get; set; }

        public T GetComponent<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            if (includeItself && this is T)
                return this as T;

            return Component as T;
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

        public T GetComponentInChildren<T>(bool includeItself = false)
            where T : class, TAbstract
        {
            var component = GetComponent<T>(includeItself);
            if (component != null)
                return component;

            return Component != null ? Component.GetComponentInChildren<T>() : null;
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