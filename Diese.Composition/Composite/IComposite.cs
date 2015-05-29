using System.Collections.Generic;

namespace Diese.Composition.Composite
{
    public interface IComposite<TAbstract> : IComponent<TAbstract>, ICollection<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
    }
}