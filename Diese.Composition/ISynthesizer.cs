using System.Collections.Generic;

namespace Diese.Composition
{
    public interface ISynthesizer<TAbstract, out TInput> : IComponent<TAbstract>, IEnumerable<TInput>
        where TAbstract : IComponent<TAbstract>
        where TInput : TAbstract
    {
         
    }
}