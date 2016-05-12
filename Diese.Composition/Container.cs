﻿using System.Collections.Generic;
using Diese.Composition.Base;
using Diese.Composition.Exceptions;

namespace Diese.Composition
{
    public class Container<TAbstract, TParent, TComponent> : ComponentEnumerable<TAbstract, TParent, TComponent>, IContainer<TAbstract, TParent, TComponent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
        where TComponent : class, TAbstract
    {
        protected ComponentCollection<TAbstract, TParent, TComponent> Components;

        protected Container()
        {
            Components = new ComponentCollection<TAbstract, TParent, TComponent>(this);
        }

        public override IEnumerator<TComponent> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        protected override sealed void Link(TComponent component)
        {
            throw new ReadOnlyParentException();
        }

        protected override sealed void Unlink(TComponent component)
        {
            throw new ReadOnlyParentException();
        }
    }
}