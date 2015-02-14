using System;
using System.Windows.Forms;

namespace Diese.Debug
{
    public class UserExceptionPresenter
    {
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;

                RefreshAll();
            }
        }
        protected readonly IUserExceptionView View;
        private Exception _exception;

        public UserExceptionPresenter(IUserExceptionView view)
        {
            View = view;

            View.CopyButton.Click += CopyButtonOnClick;
            View.QuitButton.Click += QuitButtonOnClick;
        }

        protected virtual void RefreshAll()
        {
            View.NameLabel.Text = "Exception : " + _exception.GetType().Name;
            View.MessageLabel.Text = _exception.Message;
        }

        private void CopyButtonOnClick(object sender, EventArgs eventArgs)
        {
            var textTemplate = new ExceptionTextTemplate(_exception);
            Clipboard.SetText(textTemplate.TransformText());
        }

        private void QuitButtonOnClick(object sender, EventArgs eventArgs)
        {
            View.Close();
        }
    }

    public interface IUserExceptionView
    {
        Button CopyButton { get; }
        Button QuitButton { get; }
        Label NameLabel { get; }
        Label MessageLabel { get; }
        void Close();
    }
}