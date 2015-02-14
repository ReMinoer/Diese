using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diese.Debug
{
    public partial class UserExceptionView : Form, IUserExceptionView
    {
        public Exception Exception { get { return _presenter.Exception; } set { _presenter.Exception = value; } }
        private readonly UserExceptionPresenter _presenter;

        public UserExceptionView()
        {
            InitializeComponent();

            _presenter = new UserExceptionPresenter(this);
            systemIcon.Image = new Icon(SystemIcons.Warning, 40, 40).ToBitmap();
        }

        public UserExceptionView(Exception exception)
            : this()
        {
            Exception = exception;
        }

        static public DialogResult ShowDialog(Exception exception)
        {
            var view = new DevExceptionView(exception);
            return view.ShowDialog();
        }

        public Button CopyButton { get { return copyButton; } }
        public Button QuitButton { get { return quitButton; } }
        public Label NameLabel { get { return nameLabel; } }
        public Label MessageLabel { get { return messageLabel; } }
    }
}