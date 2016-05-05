using System.Collections;
using System.Collections.Generic;

namespace Diese.Collections.Trackers
{
    public class ReadOnlyTracker<T> : IReadOnlyCollection<T>
        where T : class
    {
        private readonly ITracker<T> _tracker;

        public int Count
        {
            get { return _tracker.Count; }
        }

        public ReadOnlyTracker(ITracker<T> tracker)
        {
            _tracker = tracker;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_tracker).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}