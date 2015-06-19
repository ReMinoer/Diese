namespace Diese.Composition
{
    public interface IParent<TAbstract, TParent> : IComponent<TAbstract, TParent>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
        bool IsReadOnly { get; }
        void Link(TAbstract child);
        void Unlink(TAbstract child);
    }
}