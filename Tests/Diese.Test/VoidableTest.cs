using FluentAssertions;
using NUnit.Framework;

namespace Diese.Test
{
    public class VoidableTest
    {
        [Test]
        static public void DefaultConstructorTest()
        {
            var voidable = new Voidable<object>();

            voidable.Value.Should().BeNull();
            voidable.HasValue.Should().BeFalse();
        }

        [Test]
        static public void ConstructorWithValueTest()
        {
            var value = new object();
            var voidable = new Voidable<object>(value);

            voidable.Value.Should().Be(value);
            voidable.HasValue.Should().BeTrue();
        }

        [Test]
        static public void ConstructorWithNullTest()
        {
            var voidable = new Voidable<object>(null);
            
            voidable.Value.Should().BeNull();
            voidable.HasValue.Should().BeTrue();
        }

        [Test]
        static public void StaticVoidTest()
        {
            Voidable<object> voidable = Voidable<object>.Void;

            voidable.Value.Should().BeNull();
            voidable.HasValue.Should().BeFalse();
        }

        [Test]
        static public void ImplicitOperatorWithValueTest()
        {
            var value = new object();
            Voidable<object> voidable = value;

            voidable.Value.Should().Be(value);
            voidable.HasValue.Should().BeTrue();
        }

        [Test]
        static public void ImplicitOperatorWithNullTest()
        {
            Voidable<object> voidable = null;

            voidable.Value.Should().BeNull();
            voidable.HasValue.Should().BeTrue();
        }
    }
}