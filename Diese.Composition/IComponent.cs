using System;
using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IComponent<TAbstract, TParent>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
        string Name { get; }
        TParent Parent { get; set; }
        TAbstract GetComponent(string name, bool includeItself = false);
        TAbstract GetComponent(Type type, bool includeItself = false);
        T GetComponent<T>(bool includeItself = false) where T : class, TAbstract;
        TAbstract GetComponentInChildren(string name, bool includeItself = false);
        TAbstract GetComponentInChildren(Type type, bool includeItself = false);
        T GetComponentInChildren<T>(bool includeItself = false) where T : class, TAbstract;
        TAbstract GetComponentAmongParents(string name);
        TAbstract GetComponentAmongParents(Type type);
        T GetComponentAmongParents<T>() where T : class, TAbstract;
        List<TAbstract> GetAllComponents(Type type, bool includeItself = false);
        List<T> GetAllComponents<T>(bool includeItself = false) where T : class, TAbstract;
        List<TAbstract> GetAllComponentsInChildren(Type type, bool includeItself = false);
        List<T> GetAllComponentsInChildren<T>(bool includeItself = false) where T : class, TAbstract;
        bool Contains(TAbstract component);
        bool ContainsInChildren(TAbstract component);
        bool ContainsAmongParents(TAbstract component);
    }
}