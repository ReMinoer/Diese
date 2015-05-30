using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IComposite<TAbstract> : IContainer<TAbstract>, IList<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
    }
}