using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Diese.Debug
{
    static public class Log
    {
        static public bool Enable { get; set; }
        static public bool ViewInConsole { get { return _viewInConsole; } set { _viewInConsole = value; } }
        static public bool UseExecutionTime { get; set; }
        static public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                _fileStream.Close();
                CopyOutputFile(value);
                _outputPath = value;
                _fileStream = new StreamWriter(_outputPath) {AutoFlush = true};
            }
        }
        private const string DefaultOutputPath = "log.txt";
        static private readonly Stopwatch ExecutionStopwatch = new Stopwatch();
        static private StreamWriter _fileStream;
        static private bool _viewInConsole = true;

        static public void Instantiate(bool showConsole, string outputPath = DefaultOutputPath)
        {
            Enable = true;
            ExecutionStopwatch.Start();

            if (showConsole)
                ShowConsole();

            _outputPath = outputPath;
            _fileStream = new StreamWriter(_outputPath) {AutoFlush = true};
        }

        static public void Message(string message)
        {
            if (!Enable)
                return;

            string formatedTime = UseExecutionTime
                                      ? ExecutionStopwatch.Elapsed.ToString(@"hh\:mm\:ss")
                                      : DateTime.Now.ToString("HH:mm:ss");

            string line = string.Format("[{0}] {1}", formatedTime, message);

            if (ViewInConsole)
                Console.WriteLine(line);

            _fileStream.WriteLine(line);
        }

        static public void Message(string message, ConsoleColor consoleColor)
        {
            if (ViewInConsole)
            {
                Console.ForegroundColor = consoleColor;
                Message(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Message(message);
        }

        static public void Message(string message, ConsoleColor consoleColor, string prefix)
        {
            Message(string.Format("{1} {0}", message, prefix), consoleColor);
        }

        static public void Message(string message, LogTag tag)
        {
            Message(message, tag.Color, tag.Prefix);
        }

        static public void CopyOutputFile(string copyPath)
        {
            File.Copy(_outputPath, copyPath);
        }

        static public void ShowConsole()
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static private extern bool AllocConsole();

        static private string _outputPath = DefaultOutputPath;
    }
}