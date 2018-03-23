namespace Diese
{
    static public class Referencies
    {
        static public void Swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }
    }
}