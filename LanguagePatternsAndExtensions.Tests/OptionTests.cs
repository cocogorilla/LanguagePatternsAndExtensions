using System;
using System.Linq;
using System.Runtime.InteropServices;
using AutoFixture;
using Xunit;
using static LanguagePatternsAndExtensions.OptionExtensions;

namespace LanguagePatternsAndExtensions.Tests
{
    public class OptionTests
    {

        [Fact]
        public void OptionUsesAreCorrect()
        {
            var fixture = new Fixture();
            var nonnullstring = fixture.Create<string>();
            var nullstring = (string)null;
            var sut = nonnullstring.Some();
            Assert.Equal(nonnullstring, sut.Single());
            var sut2 = nullstring.Some();
            Assert.Equal(Option<string>.None(), sut2);
            var sut3 = Option<string>.Some(nonnullstring);
            Assert.Equal(sut, sut3);
            Assert.Equal(Option<int>.None(), Option<int>.None());
            Assert.Equal(Option<string>.Some(nullstring), Option<string>.None());
            Assert.Equal(None<string>(), Option<string>.None());
            Assert.Equal(None<string>(), Enumerable.Empty<string>().ToOption());
            Assert.NotEqual("test".Some(), Enumerable.Empty<string>().ToOption());
            Assert.Equal("test".Some(), "test".Some());
            Assert.Equal("test".Some(), Option<string>.Some("test"));
        }

        [Fact]
        public void OptionConversionsAreCorrect()
        {
            var fixture = new Fixture();
            var toomany = fixture.CreateMany<string>(2);
            var justright = fixture.CreateMany<string>(1);
            Assert.Throws<ArgumentException>(() =>
            {
                var test1 = new Option<string>(toomany);
            });
            Option<string> test2 = justright.ToOption();
            Assert.Equal(justright.Single(), test2.Single());
        }

        [Theory, Gen]
        public void TwoSomeOptionsSameValueAreEqual(
            string foo)
        {
            var optionOne = Option<string>.Some(foo);
            var optionTwo = Option<string>.Some(foo);

            Assert.Equal(optionOne, optionTwo);
        }

        [Theory, Gen]
        public void TwoSameValuesButStoredSeparatelyOptionsAreEqual(
            int foo,
            int goo)
        {
            foo = Math.Abs(foo);
            goo = Math.Abs(goo);
            goo = goo + (foo - goo);
            var optionOne = foo.Some();
            var optionTwo = goo.Some();

            Assert.Equal(optionOne, optionTwo);
        }

        [Theory, Gen]
        public void NoneIsNotSome(
            int foo)
        {
            var testone = foo.Some();
            var testtwo = Option<int>.None();

            Assert.NotEqual(testone, testtwo);
        }
    }
}