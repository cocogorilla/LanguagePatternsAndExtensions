using System;
using System.Threading.Tasks;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using static LanguagePatternsAndExtensions.OutcomeFactory;

namespace LanguagePatternsAndExtensions.Tests
{
    public class OutcomeOptionTests
    {
        [Theory, Gen]
        public async Task TryAsyncOutcomeOptionQueryExceptionConditionIsCorrect(
            Guid arguments,
            [Frozen] Mock<IAsyncQuery<Guid, Option<string>>> query,
            string expectedMessage,
            TryAsyncOutcomeOptionQuery<Guid, string> sut)
        {
            query.Setup(x => x.SendQuery(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception(expectedMessage));

            var actual = await sut.SendQuery(arguments);

            Assert.Empty(actual.Value);
            Assert.Equal(expectedMessage, actual.ErrorMessage);
            Assert.False(actual.Succeeded);
        }

        [Theory, Gen]
        public async Task TryAsyncOutcomeOptionQuerySuccessConditionIsCorrect(
            string arguments,
            [Frozen] Mock<IAsyncQuery<string, Option<int>>> query,
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
            Mock<IAsyncQuery<string, Option<Guid>>> dummyQuery)
        {
            var sut = TryAsyncOutcome(dummyQuery.Object);
            Assert.IsType<TryAsyncOutcomeOptionQuery<string, Guid>>(sut);
        }

        [Theory, Gen]
        public void TryOutcomeOptionQueryExceptionConditionIsCorrect(
            Guid arguments,
            [Frozen] Mock<IQuery<Guid, Option<string>>> query,
            string expectedMessage,
            TryOutcomeOptionQuery<Guid, string> sut)
        {
            query.Setup(x => x.SendQuery(It.IsAny<Guid>()))
                .Throws(new Exception(expectedMessage));

            var actual = sut.SendQuery(arguments);

            Assert.Empty(actual.Value);
            Assert.Equal(expectedMessage, actual.ErrorMessage);
            Assert.False(actual.Succeeded);
        }

        [Theory, Gen]
        public void TryOutcomeOptionQuerySuccessConditionIsCorrect(
            string arguments,
            [Frozen] Mock<IQuery<string, Option<int>>> query,
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
            Mock<IQuery<string, Option<Guid>>> dummyQuery)
        {
            var sut = TryOutcome(dummyQuery.Object);
            Assert.IsType<TryOutcomeOptionQuery<string, Guid>>(sut);
        }
    }
}
