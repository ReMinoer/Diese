using System;

namespace Diese.Exceptions
{
    public partial class ExceptionTextTemplate
    {
        private readonly Exception _exception;

        public ExceptionTextTemplate(Exception exception)
        {
            _exception = exception;
        }
    }
}