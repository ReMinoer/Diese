using System;

namespace Diese
{
    public interface IBatchTree
    {
        int CurrentDepth { get; }
        bool IsBatching { get; }
        IDisposable BeginBatch();
        void EndBatch();
    }

    public interface IBatchTree<in T> : IBatchTree
    {
        bool Batch(T item);
    }
}