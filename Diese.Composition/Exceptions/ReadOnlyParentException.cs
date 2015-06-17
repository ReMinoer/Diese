using System;

namespace Diese.Composition.Exceptions
{
    public class ReadOnlyParentException : Exception
    {
        public override string Message
        {
            get { return "Parent is read-only and can't use Link or Unlink methods."; }
        }
    }
}