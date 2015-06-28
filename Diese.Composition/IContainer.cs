namespace Diese.Composition
{
    public interface IContainer<TAbstract, TParent> : IComponentEnumerable<TAbstract, TParent, TAbstract>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
    {
    }
}