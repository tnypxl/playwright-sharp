using System.IO;
using PlaywrightSharp.Tests.BaseTests;
using PlaywrightSharp.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.Launcher
{
    ///<playwright-file>launcher.spec.js</playwright-file>
    ///<playwright-describe>Playwright.executablePath</playwright-describe>
    [Collection(TestConstants.TestFixtureCollectionName)]
    public class ExecutablePathTests : PlaywrightSharpBaseTest
    {
        /// <inheritdoc/>
        public ExecutablePathTests(ITestOutputHelper output) : base(output)
        {
        }

        ///<playwright-file>launcher.spec.js</playwright-file>
        ///<playwright-describe>browserType.executablePath</playwright-describe>
        ///<playwright-it>should work</playwright-it>
        [Retry]
        public void ShouldRejectAllPromisesWhenBrowserIsClosed() => Assert.True(File.Exists(BrowserType.ExecutablePath));
    }
}
