using System;
using FluentAssertions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using Xunit;

namespace BrowserStack.Net.Tests
{
    public class DriverOptionTests
    {

        [Theory]
        [InlineData(BrowserStackSupportedBrowsers.chrome, typeof(ChromeOptions))]
        [InlineData(BrowserStackSupportedBrowsers.edge, typeof(EdgeOptions))]
        [InlineData(BrowserStackSupportedBrowsers.firefox, typeof(FirefoxOptions))]
        [InlineData(BrowserStackSupportedBrowsers.opera, typeof(OperaOptions))]
        [InlineData(BrowserStackSupportedBrowsers.safari, typeof(SafariOptions))]
        [InlineData(BrowserStackSupportedBrowsers.internet_explorer, typeof(InternetExplorerOptions))]
        public void When_creating_options_they_are_of_the_right_type(BrowserStackSupportedBrowsers browser, Type optionType)
        {
            var options = DriverOptionFactory.Setup(browser);
            options.Should().BeAssignableTo(optionType);
        }

        [Theory]
        [InlineData(BrowserStackSupportedBrowsers.chrome)]
        [InlineData(BrowserStackSupportedBrowsers.edge)]
        [InlineData(BrowserStackSupportedBrowsers.firefox)]
        [InlineData(BrowserStackSupportedBrowsers.opera)]
        [InlineData(BrowserStackSupportedBrowsers.safari)]
        [InlineData(BrowserStackSupportedBrowsers.internet_explorer)]
        public void When_setting_browserStack_capabilities_they_are_global(BrowserStackSupportedBrowsers browser)
        {
            var options = DriverOptionFactory.Setup(browser);
            options.SetupDefaults();
            options.SetupBrowserStackAuth("user", "key")
                .SetupTestDetails("project", "build", "name");

            var cap = options.ToCapabilities();
            cap.HasCapability("browserstack.user").Should().BeTrue();
            cap.HasCapability("browserstack.key").Should().BeTrue();
            cap.HasCapability("browserstack.debug").Should().BeTrue();
            cap.HasCapability("browserstack.console").Should().BeTrue();

            cap.HasCapability("os").Should().BeTrue();
            cap.HasCapability("os_version").Should().BeTrue();
            cap.HasCapability("resolution").Should().BeTrue();

            cap.HasCapability("project").Should().BeTrue();
            cap.HasCapability("build").Should().BeTrue();
            cap.HasCapability("name").Should().BeTrue();

        }
    }
}