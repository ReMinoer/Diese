using System;

namespace Diese.UnitTest
{
    public class EventTest<TEventArgs>
        where TEventArgs : EventArgs
    {
        public bool HasOccured { get; private set; }
        public object Sender { get; private set; }
        public TEventArgs Args { get; private set; }

        public EventTest()
        {
            Reset();
        }

        public void Reset()
        {
            HasOccured = false;
            Sender = null;
            Args = null;
        }

        public void OnTriggered(object sender, TEventArgs args)
        {
            HasOccured = true;
            Sender = sender;
            Args = args;
        }
    }
}