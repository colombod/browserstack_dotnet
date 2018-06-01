using FluentAssertions;
using OpenQA.Selenium.Remote;
using Xunit;

namespace BrowserStack.Net.Tests
{
    public class DesiredCapabilitiesExtensionsTests
    {
        [Fact]
        public void Capabilities_are_clone_during_setup()
        {
            var baseCap = new DesiredCapabilities();
            var defaultCap = baseCap.SetupDefaults();

            baseCap.Should().NotBeSameAs(defaultCap);
        }
    }
}
