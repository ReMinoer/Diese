using System.Collections.Generic;

namespace Diese.Composition
{
    public interface IComponentEnumerable<TAbstract, TParent, out TInput> : IParent<TAbstract, TParent>,
        IEnumerable<TInput>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
        where TInput : TAbstract
    {
    }
}