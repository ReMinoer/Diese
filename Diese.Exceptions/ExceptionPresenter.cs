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
            _view.TextBox.Text = _exception.Message;
        }

        private void OkButtonOnClick(object sender, EventArgs eventArgs)
        {
            _view.Close();
        }
    }

    public interface IExceptionView
    {
        Button OkButton { get; }
        TextBox TextBox { get; }
        void Close();
    }
}