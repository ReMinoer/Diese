namespace Diese.Composition
{
    public interface ISynthesizer<TAbstract, TParent, out TInput> : IComponentEnumerable<TAbstract, TParent, TInput>
        where TAbstract : IComponent<TAbstract, TParent>
        where TParent : IParent<TAbstract, TParent>
        where TInput : TAbstract
    {
    }
}