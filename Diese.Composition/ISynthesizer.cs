using System.Collections.Generic;

namespace Diese.Composition
{
    public interface ISynthesizer<TAbstract, out TInput> : IEnumerable<TInput>, IParent<TAbstract>
        where TAbstract : IComponent<TAbstract>
        where TInput : TAbstract
    {
    }
}