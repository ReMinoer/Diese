using System;
using System.Diagnostics;

namespace Diese.Scripting.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var scriptLoader = new ScriptLoader();

            const string filename = @"..\..\Runtime\RuntimeScript.cs";
            const string typeName = "Diese.Scripting.Sample.Runtime.RuntimeScript";

            var stopwatch =  new Stopwatch();

            ConsoleKeyInfo key;
            do
            {
                try
                {
                    stopwatch.Restart();
                    Type type = scriptLoader.LoadTypeFromFile(filename, typeName);
                    var action = (Action)Delegate.CreateDelegate(typeof(Action), type.GetMethod("Do"));
                    action();
                    stopwatch.Stop();
                    Console.WriteLine("Time : {0} ms", stopwatch.ElapsedMilliseconds);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                key = Console.ReadKey();

            } while (key.Key != ConsoleKey.Escape);
        }
    }
}
