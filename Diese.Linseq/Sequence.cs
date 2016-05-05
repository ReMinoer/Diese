using System.Collections.Generic;
using System.Linq;

namespace Diese.Linseq
{
    static public class Sequence
    {
        static public IEnumerable<T> StartWith<T>(params T[] values)
        {
            return Enumerable.Empty<T>().ThenAdd(values);
        }
    }
}