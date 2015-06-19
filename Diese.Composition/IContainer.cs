using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IContainer<TAbstract, TParent> : IEnumerable<TAbstract>, IParent<TAbstract, TParent>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
    }
}