namespace Diese.Composition
{
    public interface IDecorator<TAbstract, TParent, TComponent> : IParent<TAbstract, TParent>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
        where TComponent : TAbstract
    {
        TComponent Component { get; set; }
    }
}