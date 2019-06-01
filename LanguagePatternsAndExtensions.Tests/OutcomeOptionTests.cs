using System;
using System.Threading.Tasks;
using Moq;
using AutoFixture.Xunit2;
using Xunit;
using static LanguagePatternsAndExtensions.OutcomeFactory;

namespace LanguagePatternsAndExtensions.Tests
{
    public class OutcomeOptionTests
    {
        [Theory, Gen]
        public async Task TryAsyncOutcomeOptionQueryExceptionConditionIsCorrect(
            Guid arguments,
            [Frozen] Mock<IAsyncOutcomeQuery<Guid, Option<string>>> query,
            string expectedMessage,
            TryAsyncOutcomeOptionQuery<Guid, string> sut)
        {
            query.Setup(x => x.SendQuery(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception(expectedMessage));

            var actual = await sut.SendQuery(arguments);

            Assert.Equal(expectedMessage, actual.ErrorMessage);
            Assert.False(actual.Succeeded);
        }

        [Theory, Gen]
        public async Task TryAsyncOutcomeOptionQuerySuccessConditionIsCorrect(
            string arguments,
            [Frozen] Mock<IAsyncOutcomeQuery<string, Option<int>>> query,
            Option<int> expected,
            TryAsyncOutcomeOptionQuery<string, int> sut)
        {
            query.Setup(x => x.SendQuery(arguments))
                .ReturnsAsync(Success.Of(expected));

            var actual = await sut.SendQuery(arguments);

            Assert.Equal(expected, actual.Value);
            Assert.True(actual.Succeeded);
        }

        [Theory, Gen]
        public void TryAsyncOutcomeOptionQueryCreateInfersCorrectly(
            Mock<IAsyncOutcomeQuery<string, Option<Guid>>> dummyQuery)
        {
            var sut = TryAsyncOutcome(dummyQuery.Object);
            Assert.IsType<TryAsyncOutcomeOptionQuery<string, Guid>>(sut);
        }

        [Theory, Gen]
        public void TryOutcomeOptionQueryExceptionConditionIsCorrect(
            Guid arguments,
            [Frozen] Mock<IOutcomeQuery<Guid, Option<string>>> query,
            string expectedMessage,
            TryOutcomeOptionQuery<Guid, string> sut)
        {
            query.Setup(x => x.SendQuery(It.IsAny<Guid>()))
                .Throws(new Exception(expectedMessage));

            var actual = sut.SendQuery(arguments);

            Assert.Equal(expectedMessage, actual.ErrorMessage);
            Assert.False(actual.Succeeded);
        }

        [Theory, Gen]
        public void TryOutcomeOptionQuerySuccessConditionIsCorrect(
            string arguments,
            [Frozen] Mock<IOutcomeQuery<string, Option<int>>> query,
            Option<int> expected,
            TryOutcomeOptionQuery<string, int> sut)
        {
            query.Setup(x => x.SendQuery(arguments))
                .Returns(Success.Of(expected));

            var actual = sut.SendQuery(arguments);

            Assert.Equal(expected, actual.Value);
            Assert.True(actual.Succeeded);
        }

        [Theory, Gen]
        public void TryOutcomeOptionQueryCreateInfersCorrectly(
            Mock<IOutcomeQuery<string, Option<Guid>>> dummyQuery)
        {
            var sut = TryOutcome(dummyQuery.Object);
            Assert.IsType<TryOutcomeOptionQuery<string, Guid>>(sut);
        }
    }
}
