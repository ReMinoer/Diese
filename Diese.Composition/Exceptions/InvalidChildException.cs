using System;

namespace Diese.Composition.Exceptions
{
    public class InvalidChildException : Exception
    {
        public InvalidChildException(string message)
            : base(message)
        {
        }
    }
}