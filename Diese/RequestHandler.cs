using System;

namespace Diese
{
    public class RequestHandler<TRequestArgs, TFinalizeArgs>
        where TRequestArgs : EventArgs
    {
        public EventHandler<TFinalizeArgs> Finalize { get; set; }
        public event EventHandler<TRequestArgs> Event;

        public RequestHandler(EventHandler<TFinalizeArgs> finalize)
        {
            Finalize = finalize;
        }

        public void Invoke(object sender, TRequestArgs requestArgs)
        {
            Event?.Invoke(sender, requestArgs);
        }
    }
}