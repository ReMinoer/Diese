namespace Diese.Composition
{
    public interface IDecorator<TAbstract, TComponent> : IParent<TAbstract>
        where TAbstract : IComponent<TAbstract>
        where TComponent : TAbstract
    {
        TComponent Component { get; set; }
    }
}