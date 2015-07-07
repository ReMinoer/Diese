namespace Diese.Injection.Test.Samples
{
    public enum PlayerId
    {
        One,
        Two
    }

    public interface IPlayer : ICharacter
    {
    }

    public class Player : IPlayer
    {
    }
}