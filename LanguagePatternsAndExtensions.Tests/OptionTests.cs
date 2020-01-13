using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AutoFixture;
using Xunit;
using static LanguagePatternsAndExtensions.Option<string>;

namespace LanguagePatternsAndExtensions.Tests
{
    public class TestClass
    {
        public string Whatever { get; set; }
    }

    public class OptionTests
    {

        [Fact]
        public void OptionUsesAreCorrect()
        {
            var fixture = new Fixture();
            var nonnullstring = fixture.Create<string>();
            var nullstring = (string)null;
            var sut = nonnullstring.ToOption();
            var outcome = sut.Match(
                "", x => x);
            var apples = "apples".ToOption();
            Assert.Equal("apples", apples.Traverse(x => x));
            Assert.Throws<NullReferenceException>(() =>
            {
                string empty = null;
                var foo = empty.ToOption();
                var fail = foo.Traverse(x => x.Length);
            });
            Assert.Equal(nonnullstring, outcome);
            var sut2 = nullstring.ToOption();
            Assert.Equal(None(), sut2);
            var sut3 = Some(nonnullstring);
            Assert.Equal(sut, sut3);
            Assert.Equal(Option<int>.None(), Option<int>.None());
            Assert.Equal(Some(nullstring), None());
            Assert.Equal(None(), None());
            Assert.Equal(Option<IEnumerable<string>>.None(), ((IEnumerable<string>)null).ToOption());
            Assert.NotEqual("test".ToOption(), string.Empty.ToOption());
            Assert.Equal("test".ToOption(), "test".ToOption());
            Assert.Equal("test".ToOption(), Some("test"));
        }

        [Theory, Gen]
        public void TwoSomeOptionsSameValueAreEqual(
            string foo)
        {
            var optionOne = Some(foo);
            var optionTwo = Some(foo);

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
            var optionOne = foo.ToOption();
            var optionTwo = goo.ToOption();

            Assert.Equal(optionOne, optionTwo);
        }

        [Theory, Gen]
        public void NoneIsNotSome(
            int foo)
        {
            var testone = foo.ToOption();
            var testtwo = Option<int>.None();

            Assert.NotEqual(testone, testtwo);
        }

        [Theory, Gen]
        public void NoneIsNoneSetCorrectly()
        {
            var foo = Option<int>.None();

            Assert.True(foo.IsNone);
            Assert.False(foo.IsSome);
        }

        [Theory, Gen]
        public void CanUseParsing(
            TestClass input)
        {
            var dictionary = new ConcurrentDictionary<int, TestClass>();
            dictionary.AddOrUpdate(4, x => input, (x, y) => input);
            Option<TestClass> final;
            if (dictionary.TryGetValue(4, out TestClass found))
            {
                final = Option<TestClass>.Some(found);
            }
            else
            {
                final = Option<TestClass>.None();
            }
            Assert.True(final.IsSome);
            Assert.False(final.IsNone);
        }
    }
}