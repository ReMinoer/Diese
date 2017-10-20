using System.Collections.Generic;

namespace Diese.Collections
{
    static public class EnumeratorExtension
    {
        static public IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}