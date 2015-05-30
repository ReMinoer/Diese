using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IContainer<TAbstract> : IComponent<TAbstract>, IEnumerable<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
    }
}