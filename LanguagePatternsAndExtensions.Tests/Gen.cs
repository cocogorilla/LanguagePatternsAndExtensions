using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace LanguagePatternsAndExtensions.Tests
{
    public class Gen : AutoDataAttribute
    {
        public Gen()
            : this(connectionString: null)
        { }
        public Gen(string connectionString = null) : base(new Fixture().Customize(
            new CompositeCustomization(
                new OptionIntCustomization(),
                new AutoMoqCustomization())))
        { }
    }

    public class OptionIntCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Option<int>>(c => c.FromFactory(() => new Option<int>(fixture.CreateMany<int>(1))));
        }
    }
}
