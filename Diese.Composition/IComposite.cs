using System.Collections.Generic;

namespace Diese.Composition
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IComposite<TAbstract, TParent> : IList<TAbstract>, IParent<TAbstract, TParent>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
    }
}