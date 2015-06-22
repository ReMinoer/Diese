using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IOrderedComposite<TAbstract, TParent> : IComposite<TAbstract, TParent>, IList<TAbstract>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
    }
}