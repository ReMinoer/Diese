namespace Diese
{
    public interface IDefaultable
    {
        void SetToDefault();
    }

    public interface IDefaultable<T> : IDefaultable
    {
        T DefaultValue { get; set; }
    }
}