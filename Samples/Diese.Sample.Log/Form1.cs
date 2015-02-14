using System.Windows.Forms;
using Diese.Debug;

namespace Diese.Sample.Log
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Debug.Log.Message("Run program.exe...", DefaultLogTag.System);
            Debug.Log.Message("I'm a system message.", DefaultLogTag.System);
            Debug.Log.Message("I'm a warning.", DefaultLogTag.Warning);
            Debug.Log.Message("I'm an error.", DefaultLogTag.Error);
            Debug.Log.Message("I'm a debug message.", DefaultLogTag.Debug);

            Debug.Log.Enable = false;
            Debug.Log.Message("I'm hide.", DefaultLogTag.Debug);
            Debug.Log.Enable = true;

            Debug.Log.UseExecutionTime = true;
            Debug.Log.Message("I use the execution time.", DefaultLogTag.Debug);
            Debug.Log.UseExecutionTime = false;
            Debug.Log.Message("I use the real time.", DefaultLogTag.Debug);

            Debug.Log.Message("I'm a normal message.");
        }
    }
}