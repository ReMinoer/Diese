using Diese.Injection.Test.Samples;
using NUnit.Framework;

namespace Diese.Injection.Test
{
    [TestFixture]
    public class InjectionTest
    {
        private IDependencyInjector _injector;
        private IDependencyRegistry _registry;

        [SetUp]
        public void SetUp()
        {
            _registry = new DependencyRegistry();
            _injector = new RegistryInjector(_registry);
        }

        [Test]
        public void RegisterType()
        {
            _registry.Register<Player>();

            var player = _injector.Resolve<Player>();
            var otherPlayer = _injector.Resolve<Player>();

            Assert.IsNotNull(player);
            Assert.AreNotEqual(player, otherPlayer);
        }

        [Test]
        public void RegisterImplementationA()
        {
            _registry.Register<ICharacter, Player>();

            var character = _injector.Resolve<ICharacter>();

            Assert.IsTrue(character is Player);
        }

        [Test]
        public void RegisterImplementationB()
        {
            _registry.Register<ICharacter>(typeof(Player));

            object character = _injector.Resolve(typeof(ICharacter));

            Assert.IsTrue(character is Player);
        }

        [Test]
        public void RegisterImplementationC()
        {
            _registry.Register(typeof(ICharacter), typeof(Player));

            object character = _injector.Resolve(typeof(ICharacter));

            Assert.IsTrue(character is Player);
        }

        [Test]
        public void RegisterInstance()
        {
            var alpha = new Character();
            _registry.RegisterInstance<ICharacter>(alpha);

            var resolvedAlpha = _injector.Resolve<ICharacter>();

            Assert.AreEqual(alpha, resolvedAlpha);
        }

        [Test]
        public void RegisterSingleton()
        {
            _registry.Register<Game>(Subsistence.Singleton);

            var game = _injector.Resolve<Game>();
            var otherGame = _injector.Resolve<Game>();

            Assert.AreEqual(game, otherGame);
        }

        [Test]
        public void RegisterKeyed()
        {
            _registry.Register<IPlayer, Player>(Subsistence.Singleton, PlayerId.One);
            _registry.Register<IPlayer, Player>(Subsistence.Singleton, PlayerId.Two);

            var playerOne = _injector.Resolve<IPlayer>(PlayerId.One);
            var otherPlayerOne = _injector.Resolve<IPlayer>(PlayerId.One);
            var playerTwo = _injector.Resolve<IPlayer>(PlayerId.Two);
            var otherPlayerTwo = _injector.Resolve<IPlayer>(PlayerId.Two);

            Assert.AreEqual(playerOne, otherPlayerOne);
            Assert.AreEqual(playerTwo, otherPlayerTwo);

            Assert.AreNotEqual(playerOne, playerTwo);
            Assert.AreNotEqual(otherPlayerOne, otherPlayerTwo);
            Assert.AreNotEqual(playerOne, otherPlayerTwo);
            Assert.AreNotEqual(playerTwo, otherPlayerOne);
        }

        [Test]
        public void RegisterWithDependencies()
        {
            _registry.Register<ILevel, Level>();
            _registry.Register<IPlayer, Player>();
            _registry.Register<Game>(Subsistence.Singleton);

            var level = _injector.Resolve<ILevel>();

            Assert.IsNotNull(level);
            Assert.IsNotNull(level.Game);
            Assert.IsNotNull(level.Player);
        }

        [Test]
        public void InjectPropertyAndField()
        {
            _registry.Register<Level>();
            _registry.Register<IPlayer, Player>();
            _registry.Register<Game>(Subsistence.Singleton);

            var level = _injector.Resolve<Level>();

            Assert.IsNotNull(level);
            Assert.IsNotNull(level.Game);
            Assert.IsNotNull(level.Player);
            Assert.IsNull(level.CharacterA);
            Assert.IsNull(level.CharacterB);

            _registry.Register<ICharacter, Character>();

            var otherLevel = _injector.Resolve<Level>();

            Assert.IsNotNull(otherLevel);
            Assert.IsNotNull(otherLevel.Game);
            Assert.IsNotNull(otherLevel.Player);
            Assert.IsNotNull(otherLevel.CharacterA);
            Assert.IsNotNull(otherLevel.CharacterB);
        }
    }
}