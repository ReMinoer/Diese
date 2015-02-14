using System;

namespace Diese.Debug
{
    public struct LogTag
    {
        public ConsoleColor Color { get; set; }
        public string Prefix { get; set; }

        public LogTag(ConsoleColor color, string prefix = "")
            : this()
        {
            Color = color;
            Prefix = prefix;
        }
    }
}