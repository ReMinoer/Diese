using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Diese.Debug
{
    public class DevExceptionPresenter : UserExceptionPresenter
    {
        private readonly IDevExceptionView _view;

        public DevExceptionPresenter(IDevExceptionView view)
            : base(view)
        {
            _view = view;
        }

        protected override void RefreshAll()
        {
            base.RefreshAll();

            var stackTrace = new StackTrace(Exception, true);
            StackFrame[] stackFrames = stackTrace.GetFrames();

            if (stackFrames == null)
                return;

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
                    new ListViewItem.ListViewSubItem
                    {
                        Text = ""
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Text = stackFrame.GetMethod().ToString()
                    },
                    new ListViewItem.ListViewSubItem
                    {
                        Text = stackFrame.GetFileLineNumber().ToString()
                    }
                };
                var item = new ListViewItem(subItems, 0, group);
                _view.StackTraceList.Items.Add(item);
            }
        }
    }

    public interface IDevExceptionView : IUserExceptionView
    {
        ListView StackTraceList { get; }
    }
}