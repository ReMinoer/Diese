using System;
using System.Windows.Forms;
using Diese.Debug;

namespace Diese.Exceptions.Sample
{
    static internal class Program
    {
        /// <summary>
        ///     Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static private void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Application.Run(new DevExceptionView(e));
                Application.Run(new UserExceptionView(e));
            }
        }

        static public void Method1()
        {
            Method2();
        }

        static public void Method2()
        {
            Class1.Method1();
        }

        static public void Method3()
        {
            Class1.Method2();
        }

        static public void Method4()
        {
            // ReSharper disable All
            int zero = 0;
            int divideByZero = 5 / zero;
            // ReSharper restore All
        }
    }
}