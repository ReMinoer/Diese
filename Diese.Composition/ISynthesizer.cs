using System.Collections.Generic;

namespace Diese.Composition
{
    public interface ISynthesizer<TAbstract, TParent, out TInput> : IEnumerable<TInput>, IParent<TAbstract, TParent>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
        where TInput : TAbstract
    {
    }
}