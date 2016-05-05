using System.Collections.Generic;

namespace Diese.Linseq
{
    static public class LinseqNumericExtensions
    {
        static public IEnumerable<int> ThenGoTo(this IEnumerable<int> sequence, int goal, int step = 1)
        {
            return sequence.ThenGoTo(goal, current => goal.CompareTo(current) * step);
        }

        static public IEnumerable<double> ThenGoTo(this IEnumerable<double> sequence, double goal, double step = 1)
        {
            return sequence.ThenGoTo(goal, current => goal.CompareTo(current) * step);
        }

        static public IEnumerable<float> ThenGoTo(this IEnumerable<float> sequence, float goal, float step = 1)
        {
            return sequence.ThenGoTo(goal, current => goal.CompareTo(current) * step);
        }
    }
}