using System;
using System.CodeDom.Compiler;

namespace Diese.Scripting
{
    public class CompileErrorException : Exception
    {
        public CompilerErrorCollection Errors { get; set; }

        public CompileErrorException(CompilerErrorCollection errors)
        {
            Errors = errors;
        }
    }
}