using System;
using System.Windows.Forms;

namespace Diese.Exceptions
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
        protected readonly IUserExceptionView _view;
        protected Exception _exception;

        public UserExceptionPresenter(IUserExceptionView view)
        {
            _view = view;

            _view.CopyButton.Click += CopyButtonOnClick;
            _view.QuitButton.Click += QuitButtonOnClick;
        }

        protected virtual void RefreshAll()
        {
            _view.NameLabel.Text = "Exception : " + _exception.GetType().Name;
            _view.MessageLabel.Text = _exception.Message;
        }

        private void CopyButtonOnClick(object sender, EventArgs eventArgs)
        {
            var textTemplate = new ExceptionTextTemplate(_exception);
            Clipboard.SetText(textTemplate.TransformText());
        }

        private void QuitButtonOnClick(object sender, EventArgs eventArgs)
        {
            _view.Close();
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