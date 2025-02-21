namespace Fanex.Affiliate.User.Core.UnitTests.TestHelper
{
    using Core.Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Moq;

    public static class MockingUrlHelperExtension
    {
        public static void FakeGetFavoriteShortedLanguage()
        {
            var mockSystemService = new Mock<ISystemService>();
            mockSystemService.Setup(x => x.GetFavoriteShortedLanguage())
                .Returns("en");

            typeof(UrlHelperExtension)
                .GetField("systemService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.SetValue(null, mockSystemService.Object);
        }
    }
}
