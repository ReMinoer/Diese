using System;
using System.Runtime.Serialization;

namespace Diese.Exceptions
{
    public partial class ExceptionTextTemplate
    {
        private Exception _exception;

        public ExceptionTextTemplate(Exception exception)
        {
            _exception = exception;
        }
    }
}