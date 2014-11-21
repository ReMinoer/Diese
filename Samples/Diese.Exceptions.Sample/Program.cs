using System;
using System.Windows.Forms;

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
            Exception e = new ArgumentException("I'm not a real exception !");
            Application.Run(new ExceptionView(e));
        }
    }
}