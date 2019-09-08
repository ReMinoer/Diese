namespace Diese.Collections.Children
{
    public interface IParentable<in TParent>
        where TParent : class
    {
        TParent Parent { set; }
    }
}