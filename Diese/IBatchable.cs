using System;

namespace Diese
{
    public interface IBatchable
    {
        bool IsBatching { get; }
        IDisposable Batch();
        void BeginBatch();
        void EndBatch();
    }
}