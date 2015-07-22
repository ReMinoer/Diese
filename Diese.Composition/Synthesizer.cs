using System.Collections.Generic;
using System.Linq;
using Diese.Composition.Exceptions;

namespace Diese.Composition
{
    public class Synthesizer<TAbstract, TParent, TInput> : ComponentEnumerable<TAbstract, TParent, TInput>,
        ISynthesizer<TAbstract, TParent, TInput>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, IParent<TAbstract, TParent>
        where TInput : TAbstract
    {
        protected readonly TInput[] Components;

        public override sealed bool IsReadOnly
        {
            get { return true; }
        }

        protected Synthesizer(int size)
        {
            Components = new TInput[size];
        }

        public override IEnumerator<TInput> GetEnumerator()
        {
            return Components.Cast<TInput>().GetEnumerator();
        }

        public override sealed void Link(TAbstract child)
        {
            throw new ReadOnlyParentException();
        }

        public override sealed void Unlink(TAbstract child)
        {
            throw new ReadOnlyParentException();
        }
    }
}