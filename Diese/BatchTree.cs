using System;
using System.Collections.Generic;

namespace Diese
{
    public abstract class BatchTree : IBatchTree
    {
        private readonly Stack<BatchNode> _nodeStack = new Stack<BatchNode>();
        public int CurrentDepth => _nodeStack.Count;
        public bool IsBatching => CurrentDepth > 0;

        public IDisposable BeginBatch()
        {
            var instance = new BatchNode(this);
            _nodeStack.Push(instance);
            return instance;
        }

        public void EndBatch()
        {
            _nodeStack.Pop();

            if (!IsBatching)
                OnBatchEnded();
        }

        protected abstract void OnBatchEnded();

        private sealed class BatchNode : IDisposable
        {
            private readonly BatchTree _batch;

            public BatchNode(BatchTree batch)
            {
                _batch = batch;
            }

            public void Dispose()
            {
                _batch.EndBatch();
            }
        }
    }

    public abstract class BatchTree<T> : IBatchTree<T>
    {
        private readonly Stack<BatchNode> _nodeStack = new Stack<BatchNode>();
        public int CurrentDepth => _nodeStack.Count;
        public bool IsBatching => CurrentDepth > 0;

        public IDisposable BeginBatch()
        {
            var instance = new BatchNode(this);
            _nodeStack.Push(instance);
            return instance;
        }

        public void EndBatch()
        {
            OnNodeEnded(_nodeStack.Pop().Queue, CurrentDepth);
        }

        public bool Batch(T item)
        {
            if (!IsBatching)
                return false;

            _nodeStack.Peek().Queue.Enqueue(item);
            return true;
        }

        protected abstract void OnNodeEnded(Queue<T> queue, int depth);

        private sealed class BatchNode : IDisposable
        {
            private readonly BatchTree<T> _batch;
            public Queue<T> Queue { get; private set; } = new Queue<T>();

            public BatchNode(BatchTree<T> batch)
            {
                _batch = batch;
            }

            public void Dispose()
            {
                _batch.EndBatch();
            }
        }
    }
}