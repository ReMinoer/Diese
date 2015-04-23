using System.Windows.Forms;
using Diese.Debug;

namespace Diese.Sample.Log
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Debug.LogConsole.Message("Run program.exe...", DefaultLogTag.System);
            Debug.LogConsole.Message("I'm a system message.", DefaultLogTag.System);
            Debug.LogConsole.Message("I'm a warning.", DefaultLogTag.Warning);
            Debug.LogConsole.Message("I'm an error.", DefaultLogTag.Error);
            Debug.LogConsole.Message("I'm a debug message.", DefaultLogTag.Debug);

            Debug.LogConsole.Enable = false;
            Debug.LogConsole.Message("I'm hide.", DefaultLogTag.Debug);
            Debug.LogConsole.Enable = true;

            Debug.LogConsole.UseExecutionTime = true;
            Debug.LogConsole.Message("I use the execution time.", DefaultLogTag.Debug);
            Debug.LogConsole.UseExecutionTime = false;
            Debug.LogConsole.Message("I use the real time.", DefaultLogTag.Debug);

            Debug.LogConsole.Message("I'm a normal message.");
        }
    }
}