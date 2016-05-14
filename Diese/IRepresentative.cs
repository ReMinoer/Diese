namespace Diese
{
    public interface IRepresentative<T>
    {
        bool Represent(IRepresentative<T> obj);
    }
}