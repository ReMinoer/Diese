using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace Diese.Exceptions.Sample
{
    static internal class Program
    {
        /// <summary>
        ///     Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static private void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Application.Run(new ExceptionView(e));
            }
        }

        static private void Method1()
        {
            Method2();
        }

        static private void Method2()
        {
            Method3();
        }

        static private void Method3()
        {
            // ReSharper disable All
            int zero = 0;
            int divideByZero = 5 / zero;
            // ReSharper restore All
        }
    }
}