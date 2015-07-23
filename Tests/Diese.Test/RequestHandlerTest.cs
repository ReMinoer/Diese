using System;
using NUnit.Framework;

namespace Diese.Test
{
    public class RequestHandlerTest
    {
        private RequestHandler<RequestEventArgs, FinalizeEventArgs> _request;
        private string _result;
        private event EventHandler<FinalizeEventArgs> ProcessCompleted;

        [Test]
        public void RequestTest()
        {
            _request = new RequestHandler<RequestEventArgs, FinalizeEventArgs>(Finalize);

            _request.Event += (sender, args) => { Process(args.Number); };

            ProcessCompleted += _request.Finalize;

            _request.Invoke(this, new RequestEventArgs(10));

            Assert.AreEqual(_result, 10.ToString());
        }

        private void Process(int number)
        {
            if (ProcessCompleted != null)
                ProcessCompleted.Invoke(this, new FinalizeEventArgs(number.ToString()));
        }

        private void Finalize(object sender, FinalizeEventArgs eventArgs)
        {
            _result = eventArgs.Text;
        }

        private sealed class RequestEventArgs : EventArgs
        {
            public int Number { get; private set; }

            public RequestEventArgs(int number)
            {
                Number = number;
            }
        }

        private sealed class FinalizeEventArgs : EventArgs
        {
            public string Text { get; private set; }

            public FinalizeEventArgs(string text)
            {
                Text = text;
            }
        }
    }
}