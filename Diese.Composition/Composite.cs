using System;
using System.Collections.Generic;

namespace Diese.Composition
{
    public class Composite<TAbstract> : ComponentEnumerable<TAbstract, TAbstract>, IComposite<TAbstract>
        where TAbstract : class, IComponent<TAbstract>
    {
        protected readonly List<TAbstract> Components;
        public override string Name { get; set; }

        public int Count
        {
            get { return Components.Count; }
        }

        public override sealed bool IsReadOnly
        {
            get { return false; }
        }

        protected Composite()
        {
            Components = new List<TAbstract>();
        }

        public virtual void Add(TAbstract item)
        {
            if (item.ContainsComponentInChildren(this))
                throw new InvalidOperationException("Item have its parent in its children !");

            Components.Add(item);
        }

        public virtual void Clear()
        {
            Components.Clear();
        }

        public bool Contains(TAbstract item)
        {
            return Components.Contains(item);
        }

        public void CopyTo(TAbstract[] array, int arrayIndex)
        {
            Components.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(TAbstract item)
        {
            return Components.Remove(item);
        }

        public override IEnumerator<TAbstract> GetEnumerator()
        {
            return Components.GetEnumerator();
        }

        public int IndexOf(TAbstract item)
        {
            return Components.IndexOf(item);
        }

        public virtual void Insert(int index, TAbstract item)
        {
            Components.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            Components.RemoveAt(index);
        }

        public virtual TAbstract this[int index]
        {
            get { return Components[index]; }
            set { Components[index] = value; }
        }

        public override sealed void Link(TAbstract child)
        {
            Add(child);
            child.Parent = this;
        }

        public override sealed void Unlink(TAbstract child)
        {
            Remove(child);
            child.Parent = null;
        }
    }
}