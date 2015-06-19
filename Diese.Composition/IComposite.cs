using System.Collections.Generic;

namespace Diese.Composition
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IComposite<TAbstract, TParent> : IContainer<TAbstract, TParent>, IList<TAbstract>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
    }
}