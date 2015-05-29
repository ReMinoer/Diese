using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IComponent<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
        string Name { get; }
        T GetComponent<T>(bool includeItself = false) where T : class, TAbstract;
        T GetComponentInChildren<T>(bool includeItself = false) where T : class, TAbstract;
        List<T> GetAllComponents<T>(bool includeItself = false) where T : class, TAbstract;
        List<T> GetAllComponentsInChildren<T>(bool includeItself = false) where T : class, TAbstract;
        bool ContainsComponent(IComponent<TAbstract> component);
        bool ContainsComponentInChildren(IComponent<TAbstract> component);
    }
}