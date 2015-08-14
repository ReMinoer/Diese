using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IComposite<TAbstract, TParent, TComponent> : IParent<TAbstract, TParent>, IEnumerable<TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
        void Add(TComponent component);
        void Remove(TComponent component);
        void Clear();
    }
}