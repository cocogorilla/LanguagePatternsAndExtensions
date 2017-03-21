using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
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

        [Theory, Gen]
        public async Task TryOutcomeQueryExceptionConditionIsCorrect(
            Guid arguments,
            [Frozen] Mock<IQuery<Guid, IEnumerable<string>>> query,
            string expectedMessage,
            TryOutcomeQuery<Guid, string> sut)
        {
            query.Setup(x => x.SendQuery(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception(expectedMessage));

            var actual = await sut.SendQuery(arguments);

            Assert.Empty(actual.Value);
            Assert.Equal(expectedMessage, actual.ErrorMessage);
            Assert.False(actual.Succeeded);
        }

        [Theory, Gen]
        public async Task TryOutcomeQuerySuccessConditionIsCorrect(
            string arguments,
            [Frozen] Mock<IQuery<string, IEnumerable<int>>> query,
            IEnumerable<int> expected,
            TryOutcomeQuery<string, int> sut)
        {
            query.Setup(x => x.SendQuery(arguments))
                .ReturnsAsync(Success.Of(expected));

            var actual = await sut.SendQuery(arguments);

            Assert.Equal(expected, actual.Value);
            Assert.True(actual.Succeeded);
        }

        [Theory, Gen]
        public async Task TryOutcomeCommandExceptionConditionIsCorrect(
            Guid arguments,
            [Frozen] Mock<ICommand<Guid>> command,
            string expectedMessage,
            TryOutcomeCommand<Guid> sut)
        {
            command.Setup(x => x.SendCommand(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception(expectedMessage));

            var actual = await sut.SendCommand(arguments);

            Assert.Equal(Failure.Of(Unit.Default, expectedMessage), actual);
        }

        [Theory, Gen]
        public async Task TryOutcomeCommandSuccessConditionIsCorrect(
            long arguments,
            [Frozen] Mock<ICommand<long>> command,
            TryOutcomeCommand<long> sut)
        {
            command.Setup(x => x.SendCommand(arguments))
                .ReturnsAsync(Success.Of(Unit.Default));

            var actual = await sut.SendCommand(arguments);

            Assert.Equal(Success.Of(Unit.Default), actual);
        }
    }
}
