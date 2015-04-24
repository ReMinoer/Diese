﻿using System.Runtime.InteropServices;

namespace Diese.Debug
{
    static public class LogConsole
    {
        static public void Instantiate()
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static private extern bool AllocConsole();
    }
}