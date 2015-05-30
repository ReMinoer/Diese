namespace Diese.Composition
{
    public interface IDecorator<TAbstract> : IComponent<TAbstract>
        where TAbstract : IComponent<TAbstract>
    {
        TAbstract Component { get; set; }
    }
}