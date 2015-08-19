using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IContainer<TAbstract, TParent, out TComponent> : IParent<TAbstract, TParent>, IEnumerable<TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
    }
}