using System.Collections.Generic;
using System.Linq;
using Diese.Composition.Exceptions;

namespace Diese.Composition
{
    public class Synthesizer<TAbstract, TInput> : ComponentEnumerable<TAbstract, TInput>,
        ISynthesizer<TAbstract, TInput>
        where TAbstract : class, IComponent<TAbstract>
        where TInput : TAbstract
    {
        protected readonly TInput[] Components;
        public override sealed string Name { get; set; }

        protected Synthesizer(int size)
        {
            Components = new TInput[size];
        }

        public override IEnumerator<TInput> GetEnumerator()
        {
            return Components.Cast<TInput>().GetEnumerator();
        }

        public override sealed bool IsReadOnly
        {
            get { return true; }
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