using System.Collections.Generic;

namespace Diese.Composition
{
    public class Container<TAbstract> : ComponentEnumerable<TAbstract>, IContainer<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
        protected readonly TAbstract[] Components;
        public sealed override string Name { get; set; }

        protected Container(int size)
        {
            Components = new TAbstract[size];
        }

        public override IEnumerator<TAbstract> GetEnumerator()
        {
            return ((IEnumerable<TAbstract>)Components).GetEnumerator();
        }
    }
}