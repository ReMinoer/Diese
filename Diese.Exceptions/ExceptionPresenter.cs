using System;
using System.Diagnostics;
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

            _view.CopyButton.Click += CopyButtonOnClick;
            _view.QuitButton.Click += QuitButtonOnClick;
        }

        private void RefreshAll()
        {
            _view.NameLabel.Text = "Exception : " + _exception.GetType().Name;
            _view.MessageLabel.Text = _exception.Message;

            var stackTrace = new StackTrace(_exception, true);
            var stackFrames = stackTrace.GetFrames();
            if (stackFrames != null)
            {
                ListViewGroup group = null;
                foreach (StackFrame stackFrame in stackFrames)
                {
                    Type reflectedType = stackFrame.GetMethod().ReflectedType;
                    if (reflectedType != null)
                    {
                        string groupName = string.Format("{0} ({1})", reflectedType.FullName, stackFrame.GetFileName());
                        if (group == null || group.Name != groupName)
                            group = _view.StackTraceList.Groups.Add(groupName, groupName);
                    }

                    var subItems = new[]
                    {
                        new ListViewItem.ListViewSubItem {Text = ""},
                        new ListViewItem.ListViewSubItem {Text = stackFrame.GetMethod().ToString()},
                        new ListViewItem.ListViewSubItem {Text = stackFrame.GetFileLineNumber().ToString()}
                    };
                    var item = new ListViewItem(subItems, 0, group);
                    _view.StackTraceList.Items.Add(item);
                }
            }
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

    public interface IExceptionView
    {
        Button CopyButton { get; }
        Button QuitButton { get; }
        Label NameLabel { get; }
        Label MessageLabel { get; }
        ListView StackTraceList { get; }
        void Close();
    }
}