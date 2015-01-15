using System;
using System.Windows.Forms;

namespace Diese.Exceptions
{
    public class ExceptionPresenter
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
        private readonly IExceptionView _view;
        private Exception _exception;

        public ExceptionPresenter(IExceptionView view)
        {
            _view = view;

            _view.OkButton.Click += OkButtonOnClick;
        }

        private void RefreshAll()
        {
            _view.NameLabel.Text = "An exception occurred : " + _exception.GetType().Name;
            _view.MessageLabel.Text = _exception.Message;
            _view.TextBox.Text = _exception.StackTrace;
        }

        private void OkButtonOnClick(object sender, EventArgs eventArgs)
        {
            _view.Close();
        }
    }

    public interface IExceptionView
    {
        Button OkButton { get; }
        Label NameLabel { get; }
        Label MessageLabel { get; }
        TextBox TextBox { get; }
        void Close();
    }
}