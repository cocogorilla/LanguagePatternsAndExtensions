using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace LanguagePatternsAndExtensions.Tests
{
    public class OutcomeTests
    {
        [Theory, Gen]
        public void SuccessContainsExpectedValue(
            Guid expected)
        {
            var sut = Success.Of(expected);
            Assert.Equal(expected, sut.Value);
        }

        [Fact]
        public void SuccessSuccceded()
        {
            var sut = Success.Of("anything");
            Assert.True(sut.Succeeded);
        }

        [Theory, Gen]
        public void FailureMessageIsExpected(
            string expected)
        {
            var sut = Failure.Of(42, expected);
            Assert.Equal(expected, sut.ErrorMessage);
        }

        [Fact]
        public void FailureDidNotSucceed()
        {
            var sut = Failure.Of(default(int), "some message");
            Assert.False(sut.Succeeded);
        }

        [Theory, Gen]
        public void SuccessIsGuarded(
            [Frozen] IFixture fixture,
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Success).GetMethod(nameof(Success.Of)));
        }

        [Theory, Gen]
        public void FailureIsGuarded(
            [Frozen] IFixture fixture,
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Failure).GetMethod(nameof(Failure.Of)));
        }

        [Theory, Gen]
        public void OutcomesAreGuarded(
            [Frozen] IFixture fixture,
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Outcome<string>).GetConstructors());
        }

        [Theory, Gen]
        public void SuccessOfUnitsAreEqual()
        {
            var outcome1 = Success.Of(Unit.Default);
            var outcome2 = Success.Of(Unit.Default);
            Assert.Equal(outcome1, outcome2);
        }

        [Theory, Gen]
        public void FailureOfUnitsAreEqual(
            string message)
        {
            var outcome1 = Failure.Of(Unit.Default, message);
            var outcome2 = Failure.Of(Unit.Default, message);
            Assert.Equal(outcome1, outcome2);
        }

        [Theory, Gen]
        public void SuccessfulUnitIsCorrect()
        {
            var expected = Success.Of(Unit.Default);
            var actual = Success.Ok();
            Assert.Equal(expected, actual);
        }

        [Theory, Gen]
        public void FailureUnitIsCorrect(string expectedMessage)
        {
            var expected = Failure.Of(Unit.Default, expectedMessage);
            var actual = Failure.Nok(expectedMessage);
            Assert.Equal(expected, actual);
        }
    }
}
