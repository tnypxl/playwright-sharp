using System.Threading.Tasks;
using PlaywrightSharp.Tests.BaseTests;
using PlaywrightSharp.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.BrowserContext
{
    /// <playwright-file>cookies.spec.js</playwright-file>
    /// <playwright-describe>BrowserContext.clearCookies</playwright-describe>
    [Collection(TestConstants.TestFixtureBrowserCollectionName)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1000:Test classes must be public", Justification = "Disabled")]
    class ClearCookiesTests : PlaywrightSharpPageBaseTest
    {
        /// <inheritdoc/>
        public ClearCookiesTests(ITestOutputHelper output) : base(output)
        {
        }

        /// <playwright-file>cookies.spec.js</playwright-file>
        /// <playwright-describe>BrowserContext.clearCookies</playwright-describe>
        /// <playwright-it>should clear cookies</playwright-it>
        [Retry]
        public async Task ShouldClearCookes()
        {
            await Page.GoToAsync(TestConstants.EmptyPage);
            await Context.SetCookiesAsync(new SetNetworkCookieParam
            {
                Url = TestConstants.EmptyPage,
                Name = "cookie1",
                Value = "1"
            });
            Assert.Equal("cookie1=1", await Page.EvaluateAsync<string>("document.cookie"));
            await Context.ClearCookiesAsync();
            Assert.Empty(await Page.EvaluateAsync<string>("document.cookie"));
        }

        /// <playwright-file>cookies.spec.js</playwright-file>
        /// <playwright-describe>BrowserContext.clearCookies</playwright-describe>
        /// <playwright-it>should isolate cookies when clearing</playwright-it>
        [Retry]
        public async Task ShouldIsolateWhenClearing()
        {
            await using var anotherContext = await Browser.NewContextAsync();
            await Context.SetCookiesAsync(new SetNetworkCookieParam
            {
                Name = "page1cookie",
                Value = "page1value",
                Url = TestConstants.EmptyPage
            });

            await anotherContext.SetCookiesAsync(new SetNetworkCookieParam
            {
                Name = "page2cookie",
                Value = "page2value",
                Url = TestConstants.EmptyPage
            });

            Assert.Single(await Context.GetCookiesAsync());
            Assert.Single(await anotherContext.GetCookiesAsync());

            await Context.ClearCookiesAsync();
            Assert.Empty((await Context.GetCookiesAsync()));
            Assert.Single((await anotherContext.GetCookiesAsync()));

            await anotherContext.ClearCookiesAsync();
            Assert.Empty(await Context.GetCookiesAsync());
            Assert.Empty(await anotherContext.GetCookiesAsync());
        }
    }
}
