namespace Diese.Injection.Test.Samples
{
    public interface ILevel
    {
        Game Game { get; }
        IPlayer Player { get; }
    }

    public class Level : ILevel
    {
        public Game Game { get; private set; }
        public IPlayer Player { get; private set; }

        [Injectable]
        public ICharacter CharacterA { get; set; }

        [Injectable]
        public ICharacter CharacterB;

        public Level(Game game, IPlayer player)
        {
            Game = game;
            Player = player;
        }
    }
}