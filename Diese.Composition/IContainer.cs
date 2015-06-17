using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IContainer<TAbstract> : IEnumerable<TAbstract>, IParent<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
    }
}