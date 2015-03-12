using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diese.Debug
{
    public partial class DevExceptionView : Form, IDevExceptionView
    {
        private readonly DevExceptionPresenter _presenter;

        public Exception Exception
        {
            get { return _presenter.Exception; }
            set { _presenter.Exception = value; }
        }

        public Button CopyButton
        {
            get { return copyButton; }
        }

        public Button QuitButton
        {
            get { return quitButton; }
        }

        public Label NameLabel
        {
            get { return nameLabel; }
        }

        public Label MessageLabel
        {
            get { return messageLabel; }
        }

        public ListView StackTraceList
        {
            get { return stackTraceList; }
        }

        public DevExceptionView()
        {
            InitializeComponent();

            _presenter = new DevExceptionPresenter(this);
            systemIcon.Image = new Icon(SystemIcons.Warning, 40, 40).ToBitmap();
        }

        public DevExceptionView(Exception exception)
            : this()
        {
            Exception = exception;
        }

        static public DialogResult ShowDialog(Exception exception)
        {
            var view = new DevExceptionView(exception);
            return view.ShowDialog();
        }
    }
}