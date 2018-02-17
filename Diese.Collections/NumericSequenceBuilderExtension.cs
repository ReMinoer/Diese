using System.Collections.Generic;

namespace Diese.Collections
{
    static public class NumericSequenceBuilderExtension
    {
        static public IEnumerable<int> ThenGoTo(this IEnumerable<int> sequence, int goal, int step = 1)
        {
            return sequence.ThenGoTo(goal, current => current + goal.CompareTo(current) * step);
        }

        static public IEnumerable<double> ThenGoTo(this IEnumerable<double> sequence, double goal, double step = 1)
        {
            return sequence.ThenGoTo(goal, current => current + goal.CompareTo(current) * step);
        }

        static public IEnumerable<float> ThenGoTo(this IEnumerable<float> sequence, float goal, float step = 1)
        {
            return sequence.ThenGoTo(goal, current => current + goal.CompareTo(current) * step);
        }
    }
}