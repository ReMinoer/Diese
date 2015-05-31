namespace Diese.Composition
{
    public interface IDecorator<TAbstract, TComponent> : IComponent<TAbstract>
        where TAbstract : IComponent<TAbstract>
        where TComponent : TAbstract
    {
        TComponent Component { get; set; }
    }
}