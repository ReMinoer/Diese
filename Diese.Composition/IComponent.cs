using System;
using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IComponent<TAbstract, TParent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
    {
        string Name { get; }
        TParent Parent { get; set; }
        TAbstract GetComponent(string name);
        TAbstract GetComponent(Type type);
        T GetComponent<T>() where T : class, TAbstract;
        TAbstract GetComponentInChildren(string name);
        TAbstract GetComponentInChildren(Type type);
        T GetComponentInChildren<T>() where T : class, TAbstract;
        TAbstract GetComponentAmongParents(string name);
        TAbstract GetComponentAmongParents(Type type);
        T GetComponentAmongParents<T>() where T : class, TAbstract;
        IEnumerable<TAbstract> GetAllComponents(Type type);
        IEnumerable<T> GetAllComponents<T>() where T : class, TAbstract;
        IEnumerable<TAbstract> GetAllComponentsInChildren();
        IEnumerable<TAbstract> GetAllComponentsInChildren(Type type);
        IEnumerable<T> GetAllComponentsInChildren<T>() where T : class, TAbstract;
        bool Contains(TAbstract component);
        bool ContainsInChildren(TAbstract component);
        bool ContainsAmongParents(TAbstract component);
    }
}