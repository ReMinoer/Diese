using System.Collections.Generic;
using Diese.Composition.Exceptions;

namespace Diese.Composition
{
    public class Container<TAbstract, TParent> : ComponentEnumerable<TAbstract, TParent, TAbstract>, IContainer<TAbstract, TParent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
        protected readonly TAbstract[] Components;

        public override sealed bool IsReadOnly
        {
            get { return true; }
        }

        protected Container(int size)
        {
            Components = new TAbstract[size];
        }

        public override IEnumerator<TAbstract> GetEnumerator()
        {
            return ((IEnumerable<TAbstract>)Components).GetEnumerator();
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