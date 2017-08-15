using System.Collections.Generic;

namespace Diese
{
    public class QueueBatchNode<T> : BatchNode
    {
        public Queue<T> Queue { get; } = new Queue<T>();

        public QueueBatchNode(IBatchTree batch)
            : base(batch)
        {
        }
    }
}