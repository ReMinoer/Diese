using System;

namespace Diese
{
    static public class ComparisonExtension
    {
        static public Comparison<T> Inverse<T>(this Comparison<T> comparison)
        {
            return (x, y) => comparison(x, y) * -1;
        }

        static public Func<T, T, int> Inverse<T>(this Func<T, T, int> comparison)
        {
            return (x, y) => comparison(x, y) * -1;
        }
    }
}