using System;
using System.Diagnostics;
using System.IO;
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
            _view.NameLabel.Text = "Exception : " + _exception.GetType().Name;
            _view.MessageLabel.Text = _exception.Message;

            var stackTrace = new StackTrace(_exception, true);
            StackFrame[] stackFrames = stackTrace.GetFrames();
            if (stackFrames != null)
            {
                ListViewGroup group = null;
                foreach (StackFrame stackFrame in stackFrames)
                {
                    string filename = Path.GetFileName(stackFrame.GetFileName());
                    string directory = Path.GetDirectoryName(stackFrame.GetFileName());

                    string groupName = string.Format("{0} ({1})", filename, directory);
                    if (group == null || !_view.StackTraceList.Groups.Contains(group))
                        group = _view.StackTraceList.Groups.Add(groupName, groupName);

                    var subItems = new[]
                    {
                        new ListViewItem.ListViewSubItem{ Text = "" },
                        new ListViewItem.ListViewSubItem{ Text = stackFrame.GetMethod().ToString() },
                        new ListViewItem.ListViewSubItem{ Text = stackFrame.GetFileLineNumber().ToString() },
                        new ListViewItem.ListViewSubItem{ Text = stackFrame.GetFileColumnNumber().ToString() }
                    };
                    var item = new ListViewItem(subItems, 0, group);
                    _view.StackTraceList.Items.Add(item);
                }
            }
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
        ListView StackTraceList { get; }
        void Close();
    }
}