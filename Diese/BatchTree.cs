using System;
using System.Collections.Generic;

namespace Diese
{
    public abstract class BatchTree<TBatchNode> : IBatchTree
        where TBatchNode : BatchNode
    {
        protected readonly Stack<TBatchNode> NodeStack = new Stack<TBatchNode>();
        public int BatchDepth => NodeStack.Count;
        public bool IsBatching => BatchDepth > 0;

        public IDisposable Batch()
        {
            TBatchNode instance = CreateBatchNode();
            NodeStack.Push(instance);
            return instance;
        }

        protected abstract TBatchNode CreateBatchNode();

        public void BeginBatch()
        {
            Batch();
        }

        public void EndBatch()
        {
            int batchDepth = BatchDepth;
            OnNodeEnded(NodeStack.Pop(), batchDepth);

            if (!IsBatching)
                OnBatchEnded();
        }

        protected abstract void OnNodeEnded(TBatchNode batchNode, int depth);
        protected virtual void OnBatchEnded() { }
    }
}