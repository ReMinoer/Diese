namespace Diese.Composition
{
    public interface IParent<TAbstract> : IComponent<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
        bool IsReadOnly { get; }
        void Link(TAbstract child);
        void Unlink(TAbstract child);
    }
}