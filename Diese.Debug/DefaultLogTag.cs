using System;

namespace Diese.Debug
{
    static public class DefaultLogTag
    {
        static public LogTag System = new LogTag(ConsoleColor.Green, "#");
        static public LogTag Warning = new LogTag(ConsoleColor.Yellow, "*");
        static public LogTag Error = new LogTag(ConsoleColor.Red, "!");
        static public LogTag Debug = new LogTag(ConsoleColor.Gray, "$");
    }
}