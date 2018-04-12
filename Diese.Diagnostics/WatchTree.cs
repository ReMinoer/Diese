using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace Diese.Debug
{
    public class WatchTree
    {
        private readonly Stack<DisposableWatch> _watches;
        private readonly Dictionary<DateTime, TimeNode> _results;
        private bool _enabled;
        
        public TimeNode CurrentNode { get; private set; }
        public int CurrentDepth { get; private set; }
        public IReadOnlyDictionary<DateTime, TimeNode> Results { get; private set; }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled == value)
                    return;

                _enabled = value;

                if (!_enabled)
                    Stop();
            }
        }

        public WatchTree()
        {
            CurrentDepth = -1;

            _watches = new Stack<DisposableWatch>();

            _results = new Dictionary<DateTime, TimeNode>();
            Results = new ReadOnlyDictionary<DateTime, TimeNode>(_results);
        }

        public DisposableWatch Start(string name = "")
        {
            if (!Enabled)
                return null;

            CurrentDepth++;
            CurrentNode = new TimeNode(CurrentNode, name);
            CurrentNode.Parent?.Children.Add(CurrentNode);

            if (CurrentDepth == 0)
                _results.Add(DateTime.Now, CurrentNode);

            return new DisposableWatch(this, CurrentNode);
        }

        public void ClearResults()
        {
            _results.Clear();
        }

        private void Stop()
        {
            while (CurrentDepth != -1)
                _watches.Pop().Dispose();
        }

        private void DeleteWatch()
        {
            CurrentNode = CurrentNode?.Parent;
            CurrentDepth--;
        }

        public class TimeNode
        {
            public TimeNode Parent { get; set; }
            public string Name { get; private set; }
            public TimeSpan TimeSpan { get; internal set; }
            public List<TimeNode> Children { get; private set; }

            public TimeNode(TimeNode parent, string name)
            {
                Parent = parent;
                Name = name;
                Children = new List<TimeNode>();
            }

            public void WriteAsCsv(TextWriter textWriter)
            {
                textWriter.WriteLine("Order;Name;Time (ms);Depth");

                int count = 0;
                WriteSnapshotRecursive(textWriter, this, 0, ref count);
            }

            private void WriteSnapshotRecursive(TextWriter streamWriter, TimeNode node, int depth, ref int count)
            {
                count++;

                string tabs = "";
                for (int i = 0; i < depth; i++)
                    tabs += "\t";

                streamWriter.WriteLine($"{count};{tabs}|-- {node.Name};{node.TimeSpan.TotalMilliseconds:F3};{depth}");

                foreach (TimeNode child in node.Children)
                    WriteSnapshotRecursive(streamWriter, child, depth + 1, ref count);
            }
        }

        public class DisposableWatch : Stopwatch, IDisposable
        {
            private readonly WatchTree _watchTree;
            private readonly TimeNode _node;

            internal DisposableWatch(WatchTree watchTree, TimeNode node)
            {
                _watchTree = watchTree;
                _node = node;

                _watchTree._watches.Push(this);
                
                Start();
            }

            public void Dispose()
            {
                Stop();
                _node.TimeSpan = Elapsed;

                _watchTree.DeleteWatch();
            }
        }
    }
}