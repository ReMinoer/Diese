using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diese.Exceptions
{
    public partial class ExceptionView : Form, IExceptionView
    {
        public Exception Exception { get { return _presenter.Exception; } set { _presenter.Exception = value; } }
        private readonly ExceptionPresenter _presenter;

        public ExceptionView()
        {
            InitializeComponent();

            _presenter = new ExceptionPresenter(this);
            systemIcon.Image = new Icon(SystemIcons.Warning, 40, 40).ToBitmap();
        }

        public ExceptionView(Exception exception)
            : this()
        {
            Exception = exception;
        }

        static public DialogResult ShowDialog(Exception exception)
        {
            var view = new ExceptionView(exception);
            return view.ShowDialog();
        }

        public Button CopyButton { get; private set; }
        public Button QuitButton { get; private set; }
        public Label NameLabel { get; private set; }
        public Label MessageLabel { get; private set; }
        public ListView StackTraceList { get; private set; }
    }
}