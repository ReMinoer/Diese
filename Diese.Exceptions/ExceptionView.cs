using System;
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
        }

        public ExceptionView(Exception exception)
            : this()
        {
            Exception = exception;
        }

        public Button OkButton { get { return okButton; } }
        public TextBox TextBox { get { return textBox; } }

        static public DialogResult ShowDialog(Exception exception)
        {
            var view = new ExceptionView(exception);
            return view.ShowDialog();
        }
    }
}