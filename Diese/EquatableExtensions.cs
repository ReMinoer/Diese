using System;

namespace Diese
{
    static public class EquatableExtensions
    {
        static public bool NullableEquals<T>(this IEquatable<T> value, IEquatable<T> other)
        {
            return value?.Equals(other) ?? other == null;
        }
    }
}