using Diese.Injection.Test.Samples;
using NUnit.Framework;

namespace Diese.Injection.Test
{
    [TestFixture]
    public class InjectionTest
    {
        private IDependencyInjector _injector;
        private IDependencyContainer _container;

        [SetUp]
        public void SetUp()
        {
            //_container = new DependencyContainer();
            //_injector = new DependencyInjector(_container);
        }

        [Test]
        public void RegisterType()
        {
            _container.Register<Player>();

            var player = _injector.Resolve<Player>();
            var otherPlayer = _injector.Resolve<Player>();

            Assert.IsNotNull(player);
            Assert.AreNotEqual(player, otherPlayer);
        }

        [Test]
        public void RegisterImplementationA()
        {
            _container.Register<ICharacter, Player>();

            var character = _injector.Resolve<ICharacter>();

            Assert.IsTrue(character is Player);
        }

        [Test]
        public void RegisterImplementationB()
        {
            _container.Register<ICharacter>(typeof(Player));

            object character = _injector.Resolve(typeof(ICharacter));

            Assert.IsTrue(character is Player);
        }

        [Test]
        public void RegisterImplementationC()
        {
            _container.Register(typeof(ICharacter), typeof(Player));

            object character = _injector.Resolve(typeof(ICharacter));

            Assert.IsTrue(character is Player);
        }

        [Test]
        public void RegisterInstance()
        {
            var alpha = new Character();
            _container.RegisterInstance<ICharacter>(alpha);

            var resolvedAlpha = _injector.Resolve<ICharacter>();

            Assert.AreEqual(alpha, resolvedAlpha);
        }

        [Test]
        public void RegisterSingleton()
        {
            _container.Register<Game>(Subsistence.Singleton);

            var game = _injector.Resolve<Game>();
            var otherGame = _injector.Resolve<Game>();

            Assert.AreEqual(game, otherGame);
        }

        [Test]
        public void RegisterKeyed()
        {
            _container.Register<IPlayer, Player>(Subsistence.Singleton, PlayerId.One);
            _container.Register<IPlayer, Player>(Subsistence.Singleton, PlayerId.Two);

            var playerOne = _injector.ResolveKeyed<IPlayer>(PlayerId.One);
            var otherPlayerOne = _injector.ResolveKeyed<IPlayer>(PlayerId.One);
            var playerTwo = _injector.ResolveKeyed<IPlayer>(PlayerId.Two);
            var otherPlayerTwo = _injector.ResolveKeyed<IPlayer>(PlayerId.Two);

            Assert.AreEqual(playerOne, otherPlayerOne);
            Assert.AreEqual(playerTwo, otherPlayerTwo);

            Assert.AreNotEqual(playerOne, playerTwo);
            Assert.AreNotEqual(otherPlayerOne, otherPlayerTwo);
            Assert.AreNotEqual(playerOne, otherPlayerTwo);
            Assert.AreNotEqual(playerTwo, otherPlayerOne);
        }
    }
}