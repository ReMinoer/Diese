using System.Collections.Generic;

namespace Diese.Composition
{
    public class Synthesizer<TAbstract, TInput> : ComponentEnumerable<TAbstract>, ISynthesizer<TAbstract, TInput>
        where TAbstract : IComponent<TAbstract>
        where TInput : TAbstract
    {
        protected readonly TInput[] Components;
        public override sealed string Name { get; set; }

        protected Synthesizer(int size)
        {
            Components = new TInput[size];
        }

        public override IEnumerator<TAbstract> GetEnumerator()
        {
            return (IEnumerator<TAbstract>)Components.GetEnumerator();
        }

        IEnumerator<TInput> IEnumerable<TInput>.GetEnumerator()
        {
            return (IEnumerator<TInput>)Components.GetEnumerator();
        }
    }
}