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

        public Button OkButton { get { return okButton; } }
        public Label NameLabel { get { return nameLabel; } }
        public Label MessageLabel { get { return messageLabel; } }
        public ListView StackTraceList { get { return stackTraceList; } }

        static public DialogResult ShowDialog(Exception exception)
        {
            var view = new ExceptionView(exception);
            return view.ShowDialog();
        }
    }
}