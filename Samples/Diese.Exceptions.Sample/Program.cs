using System;
using System.Linq;
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
                int zero = args.Any() ? 1 : 0;
                int divideByZero = 5 / zero;
            }
            catch (Exception e)
            {
                Application.Run(new ExceptionView(e));
            }
        }
    }
}