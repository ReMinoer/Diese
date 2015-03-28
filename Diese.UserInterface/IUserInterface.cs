namespace Diese.UserInterface
{
    public interface IUserInterface<TModel, TView>
    {
        TModel Model { get; }
        TView View { get; }

        void BindEvents(TModel model, TView view);
    }
}