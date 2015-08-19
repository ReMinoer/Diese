namespace Diese.Composition
{
    public interface IParent<TAbstract, TParent> : IComponent<TAbstract, TParent>
        where TAbstract : class, IComponent<TAbstract, TParent>
        where TParent : class, TAbstract, IParent<TAbstract, TParent>
    {
        void Link(TAbstract child);
        void Unlink(TAbstract child);
    }
}