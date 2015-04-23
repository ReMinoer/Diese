using System;
using NUnit.Framework;

namespace Diese.UnitTest.Test
{
    internal class EventTestTest
    {
        private event EventHandler<EventArgsForTest> Event;

        [Test]
        public void EventTestUse()
        {
            var eventTest = new EventTest<EventArgsForTest>();
            Event += eventTest.OnTriggered;

            Assert.IsTrue(!eventTest.HasOccured);
            Assert.IsTrue(eventTest.Sender == null);
            Assert.IsTrue(eventTest.Args == null);

            if (Event != null)
                Event.Invoke(this, new EventArgsForTest("Hello World", 10));

            Assert.IsTrue(eventTest.HasOccured);
            Assert.IsTrue(eventTest.Sender == this);
            Assert.IsTrue(eventTest.Args != null);
            Assert.IsTrue(eventTest.Args.Text == "Hello World");
            Assert.IsTrue(eventTest.Args.Number == 10);
        }

        private class EventArgsForTest : EventArgs
        {
            public string Text { get; private set; }
            public int Number { get; private set; }

            public EventArgsForTest(string text, int number)
            {
                Text = text;
                Number = number;
            }
        }
    }
}