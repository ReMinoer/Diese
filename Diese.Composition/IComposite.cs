using System.Collections.Generic;

namespace Diese.Composition
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IComposite<TAbstract, TParent> : ICollection<TAbstract>,
        IComponentEnumerable<TAbstract, TParent, TAbstract>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
    }
}