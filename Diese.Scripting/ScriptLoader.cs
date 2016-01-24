using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace Diese.Scripting
{
    public class ScriptLoader
    {
        public List<string> ReferencedAssemblies { get; private set; }
        private readonly CSharpCodeProvider _cSharpCodeProvider;

        public ScriptLoader()
        {
            _cSharpCodeProvider = new CSharpCodeProvider();
            ReferencedAssemblies = new List<string>();
        }

        public Type LoadTypeFromFile(string filename, string typeName)
        {
            var compilerParameters = new CompilerParameters
            {
                TreatWarningsAsErrors = true,
                GenerateInMemory = true,
                GenerateExecutable = false,
                IncludeDebugInformation = false
            };

            foreach (string assembly in ReferencedAssemblies)
                compilerParameters.ReferencedAssemblies.Add(assembly);

            CompilerResults result = _cSharpCodeProvider.CompileAssemblyFromFile(compilerParameters, filename);

            if (result.Errors.HasErrors)
                throw new CompileErrorException(result.Errors);

            return result.CompiledAssembly.GetType(typeName, true);
        }
    }
}
