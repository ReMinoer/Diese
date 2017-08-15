using System.Collections.Generic;
using System.Linq;

namespace Diese.Collections
{
    static public class TrackerExtension
    {
        static public void RegisterMany<T>(this ITracker<T> tracker, IEnumerable<T> items)
            where T : class
        {
            foreach (T item in items)
                tracker.Register(item);
        }

        static public bool UnregisterMany<T>(this ITracker<T> tracker, IEnumerable<T> items)
            where T : class
        {
            return items.Aggregate(true, (current, item) => current & tracker.Unregister(item));
        }
    }
}