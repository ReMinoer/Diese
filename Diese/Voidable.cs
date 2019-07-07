namespace Diese
{
    public struct Voidable<T>
    {
        static public Voidable<T> Void => new Voidable<T>();

        public bool HasValue { get; }
        public T Value { get; }

        public Voidable(T value)
        {
            Value = value;
            HasValue = true;
        }

        static public implicit operator Voidable<T>(T value) => new Voidable<T>(value);
    }
}