using System;

namespace Diese
{
    public abstract class BatchNode : IDisposable
    {
        private readonly IBatchTree _batch;

        protected BatchNode(IBatchTree batch)
        {
            _batch = batch;
        }

        public void Dispose()
        {
            _batch.EndBatch();
        }
    }
}