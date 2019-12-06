using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Xunit;

namespace LanguagePatternsAndExtensions.Tests
{
    public class OutcomeTests
    {
        [Theory, Gen]
        public void SuccessContainsExpectedValueForQuery(
            Guid expected,
            string successValue)
        {
            var sut = Success.Of(expected);
            var result = sut.Traverse(x =>
            {
                Assert.Equal(expected, x);
                return successValue;
            }, (x, y) =>
            {
                throw new Exception("should not be in this case");
            });
            Assert.Equal(result, successValue);
        }

        [Theory, Gen]
        public void SuccessContainsExpectedValueForCommand(
            Guid expected)
        {
            var sut = Success.Of(expected);
            sut.Traverse(x =>
            {
                Assert.Equal(expected, x);
            }, (x, y) => throw new Exception("should not be in this case"));
        }

        [Fact]
        public void SuccessSucceeded()
        {
            var sut = Success.Of("anything");
            Assert.True(sut.Succeeded);
        }

        [Theory, Gen]
        public void FailureMessageIsExpectedForQuery(
            string expected,
            string customError)
        {
            var sut = Failure.Of(42, expected);
            var result = sut.Traverse(x => throw new Exception("should not be in this case"),
                (x, y) =>
                {
                    Assert.Equal(expected, y);
                    return customError;
                });
            Assert.Equal(customError, result);
        }

        [Theory, Gen]
        public void FailureMessageIsExpectedForCommand(
            string expected)
        {
            var sut = Failure.Of(42, expected);
            sut.Traverse(x => throw new Exception("should not be in this case"),
                (x, y) =>
                {
                    Assert.Equal(expected, y);
                });
        }

        [Fact]
        public void FailureDidNotSucceed()
        {
            var sut = Failure.Of(default(int), "some message");
            Assert.False(sut.Succeeded);
        }

        [Theory, Gen]
        public void SuccessIsGuarded(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Success).GetMethod(nameof(Success.Of)));
        }

        [Theory, Gen]
        public void FailureIsGuarded(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Failure).GetMethod(nameof(Failure.Of)));
        }

        [Theory, Gen]
        public void OutcomesAreGuarded(
            GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Outcome<string>).GetConstructors());
        }

        [Fact]
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

        [Fact]
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
        public void OutcomeEqualitySuccessIsCorrect(string value)
        {
            var oa = Success.Of(value);
            var ob = Success.Of(value);
            Assert.True(oa == ob);
            Assert.True(oa.Equals(ob));
        }

        [Theory, Gen]
        public void HashCodeEqualityComparisonForSuccessIsCorrect(string value)
        {
            var a = Success.Of(value);
            var b = Success.Of(value);
            Assert.True(a.GetHashCode() == b.GetHashCode());
        }

        [Theory, Gen]
        public void HashCodeInEqualityComparisonForSuccessIsCorrect(string value, string value2)
        {
            var a = Success.Of(value);
            var b = Success.Of(value2);
            Assert.False(a.GetHashCode() == b.GetHashCode());
        }

        [Theory, Gen]
        public void HashCodeEqualityComparisonForFailureIsCorrect(string value, string error)
        {
            var a = Failure.Of(value, error);
            var b = Failure.Of(value, error);
            Assert.True(a.GetHashCode() == b.GetHashCode());
        }

        [Theory, Gen]
        public void HashCodeInEqualityComparisonForFailureIsCorrect(string value, string value2, string error)
        {
            var a = Failure.Of(value, error);
            var b = Failure.Of(value2, error);
            Assert.False(a.GetHashCode() == b.GetHashCode());
        }

        [Theory, Gen]
        public void HashCodeEqualityComparisonOnErrorMessageForFailureIsCorrect(string value, string error, string error2)
        {
            var a = Failure.Of(value, error);
            var b = Failure.Of(value, error2);
            Assert.True(a.GetHashCode() != b.GetHashCode());
        }

        [Fact]
        public void OutcomeUnitEqualitySuccessIsCorrect()
        {
            var oa = Success.Of(Unit.Default);
            var ob = Success.Of(Unit.Default);
            Assert.True(oa == ob);
            Assert.True(oa.Equals(ob));
        }

        [Fact]
        public void OutcomeEmptyEqualitySuccessIsCorrect()
        {
            var oa = Success.Ok();
            var ob = Success.Ok();
            Assert.True(oa == ob);
            Assert.True(oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeEqualityFailureIsCorrect(string value, string errorMessage)
        {
            var oa = Failure.Of(value, errorMessage);
            var ob = Failure.Of(value, errorMessage);
            Assert.True(oa == ob);
            Assert.True(oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeUnitEqualityFailureIsCorrect(string errorMessage)
        {
            var oa = Failure.Of(Unit.Default, errorMessage);
            var ob = Failure.Of(Unit.Default, errorMessage);
            Assert.True(oa == ob);
            Assert.True(oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeEmptyEqualityFailureIsCorrect(string errorMessage)
        {
            var oa = Failure.Nok(errorMessage);
            var ob = Failure.Nok(errorMessage);
            Assert.True(oa == ob);
            Assert.True(oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeInequalitySuccessIsCorrect(string value)
        {
            var oa = Success.Of(value);
            var ob = Success.Of(value);
            Assert.False(oa != ob);
            Assert.False(!oa.Equals(ob));
        }

        [Fact]
        public void OutcomeUnitInequalitySuccessIsCorrect()
        {
            var oa = Success.Of(Unit.Default);
            var ob = Success.Of(Unit.Default);
            Assert.False(oa != ob);
            Assert.False(!oa.Equals(ob));
        }

        [Fact]
        public void OutcomeEmptyInequalitySuccessIsCorrect()
        {
            var oa = Success.Ok();
            var ob = Success.Ok();
            Assert.False(oa != ob);
            Assert.False(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeInequalityFailureIsCorrect(string value, string errorMessage)
        {
            var oa = Failure.Of(value, errorMessage);
            var ob = Failure.Of(value, errorMessage);
            Assert.False(oa != ob);
            Assert.False(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeUnitInequalityFailureIsCorrect(string errorMessage)
        {
            var oa = Failure.Of(Unit.Default, errorMessage);
            var ob = Failure.Of(Unit.Default, errorMessage);
            Assert.False(oa != ob);
            Assert.False(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeEmptyInequalityFailureIsCorrect(string errorMessage)
        {
            var oa = Failure.Nok(errorMessage);
            var ob = Failure.Nok(errorMessage);
            Assert.False(oa != ob);
            Assert.False(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeEqualityDifferentSuccessIsCorrect(string value, string value2)
        {
            var oa = Success.Of(value);
            var ob = Success.Of(value2);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeEqualityDifferentFailureIsCorrect(string value, string value2, string errorMessage)
        {
            var oa = Failure.Of(value, errorMessage);
            var ob = Failure.Of(value2, errorMessage);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }
        
        [Theory, Gen]
        public void SuccessAndFailureAreNotEqual(string error)
        {
            var a = Success.Ok();
            var b = Failure.Nok(error);

            Assert.True(a != b);
            Assert.True(!a.Equals(b));
        }

        [Theory, Gen]
        public void AnonymousObjectComparisonFails(string error)
        {
            Assert.True(!Success.Ok().Equals(new { }));
            Assert.True(!Failure.Nok(error).Equals(new { }));
        }

        [Theory, Gen]
        public void OutcomeUnitEqualityDifferentFailureIsCorrect(string errorMessage, string errorMessage2)
        {
            var oa = Failure.Of(Unit.Default, errorMessage);
            var ob = Failure.Of(Unit.Default, errorMessage2);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeEmptyEqualityDifferentFailureIsCorrect(string errorMessage, string errorMessage2)
        {
            var oa = Failure.Nok(errorMessage);
            var ob = Failure.Nok(errorMessage2);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeInequalityDifferentSuccessIsCorrect(string value, string value2)
        {
            var oa = Success.Of(value);
            var ob = Success.Of(value2);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeInequalityDifferentFailureIsCorrect(string value, string value2, string errorMessage)
        {
            var oa = Failure.Of(value, errorMessage);
            var ob = Failure.Of(value2, errorMessage);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeUnitInequalityDifferentFailureIsCorrect(string errorMessage, string errorMessage2)
        {
            var oa = Failure.Of(Unit.Default, errorMessage);
            var ob = Failure.Of(Unit.Default, errorMessage2);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }

        [Theory, Gen]
        public void OutcomeEmptyDifferentInequalityFailureIsCorrect(string errorMessage, string errorMessage2)
        {
            var oa = Failure.Nok(errorMessage);
            var ob = Failure.Nok(errorMessage2);
            Assert.True(oa != ob);
            Assert.True(!oa.Equals(ob));
        }
    }
}
