using System;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace LanguagePatternsAndExtensions.Tests
{
    public class LifetimeManagerTests
    {
        public class TestObject
        {
            public Guid SomethingToTest { get; set; }
        }

        [Theory, Gen]
        public void IsGuarded([Frozen] IFixture fixture, GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(LifeTimeManager<TestObject>).GetConstructors());
        }

        [Theory, Gen]
        public async void NewObjectIsGeneratedForFirstCall(
            TestObject expected,
            IFixture fixture)
        {
            fixture.Inject<Func<Task<TestObject>>>(
                async () => await Task.FromResult(expected));
            var sut = fixture.Create<LifeTimeManager<TestObject>>();
            var actual = await sut.ReceiveMessage();
            Assert.Equal(expected, actual);
        }

        [Theory, Gen]
        public async void SameObjectIsGeneratedWhenNotExpired(
            TestObject expected,
            IFixture fixture)
        {
            fixture.Inject<Func<Task<TestObject>>>(
                async () => await Task.FromResult(expected));
            fixture.Inject<Func<TestObject, bool>>(
                (t) => t != expected);
            var sut = fixture.Create<LifeTimeManager<TestObject>>();

            var initial = await sut.ReceiveMessage();
            var actual = await sut.ReceiveMessage();
            var successive = await sut.ReceiveMessage();

            Assert.Equal(expected, initial);
            Assert.Equal(expected, actual);
            Assert.Equal(expected, successive);
        }

        [Theory, Gen]
        public async void NewObjectIsGeneratedWhenIsExpired(
            int expiresAt,
            TestObject oldObject,
            TestObject newObject,
            IFixture fixture)
        {
            int callCount = 0;
            fixture.Inject<Func<Task<TestObject>>>(
                async () =>
                {
                    if (callCount == 0) return await Task.FromResult(oldObject);
                    if (callCount == expiresAt) return await Task.FromResult(newObject);
                    return await Task.FromException<TestObject>(new Exception("should never have gotten here"));
                });
            fixture.Inject<Func<TestObject, bool>>(
                (t) => ++callCount == expiresAt);
            var sut = fixture.Create<LifeTimeManager<TestObject>>();

            for (var i = 0; i <= expiresAt + 5; i++)
            {
                var actual = await sut.ReceiveMessage();
                Assert.Equal(i < expiresAt ? oldObject : newObject, actual);
            }
        }
    }
}
