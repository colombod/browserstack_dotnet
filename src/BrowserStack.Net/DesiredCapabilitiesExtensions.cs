using System.Runtime.CompilerServices;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace BrowserStack.Net
{
    public static class DesiredCapabilitiesExtensions
    {
        public static DesiredCapabilities SetupDefaults(this DesiredCapabilities capabilities)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("os", "Windows");
            clone.SetCapability("os_version", "10");
            clone.SetCapability("resolution", "1024x768");
            return clone.SetupDedug(true, BrowserStackConsoleLogLevel.Info);
        }

        public static DesiredCapabilities SetupTestDetails(this DesiredCapabilities capabilities, string project = "", string build = "", [CallerMemberName]string name = null)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("project", project?? string.Empty);
            clone.SetCapability("build", build ?? string.Empty);
            clone.SetCapability("name", name ?? string.Empty);
            return clone;
        }

        public static DesiredCapabilities SetupDedug(this DesiredCapabilities capabilities, bool isEnable = false, BrowserStackConsoleLogLevel logLevel = BrowserStackConsoleLogLevel.Errors)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("browserstack.debug", isEnable.ToString().ToLower());
            clone.SetCapability("browserstack.console", logLevel.ToString().ToLower());
            return clone;
        }

        public static DesiredCapabilities SetBrowserVersion(this DesiredCapabilities capabilities, int version)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("browser_version", version.ToString("N0"));
            return clone;
        }

        public static DesiredCapabilities SetBrowser(this DesiredCapabilities capabilities, BrowserStackSupportedBrowsers browser)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("browser", browser.ToString().Replace("_", " "));
            return clone;
        }

        public static DesiredCapabilities SetupChrome(this DesiredCapabilities capabilities, int version = 64, bool enableMixedContent = false)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone
                .SetBrowser(BrowserStackSupportedBrowsers.chrome)
                .SetBrowserVersion(version);

            if (enableMixedContent)
            {
                // disable chrome security
                var options = new ChromeOptions();

                options.AddArgument("--disable-web-security");
                options.AddArgument("--allow-running-insecure-content");
                clone.SetCapability(ChromeOptions.Capability, options);
            }

            return clone;

        }

        public static DesiredCapabilities SetupEdge(this DesiredCapabilities capabilities, int version = 16)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            return clone
                .SetBrowser(BrowserStackSupportedBrowsers.edge)
                .SetBrowserVersion(version);
        }

        public static DesiredCapabilities SetupForLocalBridge(this DesiredCapabilities capabilities, string identifier)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("browserstack.local", "true");
            clone.SetCapability("browserstack.localIdentifier", identifier);
            return clone;
        }

        public static DesiredCapabilities SetupKey(this DesiredCapabilities capabilities, string key)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("browserstack.key", key);
            return clone;
        }

        public static DesiredCapabilities SetupUser(this DesiredCapabilities capabilities, string user)
        {
            var clone = new DesiredCapabilities(capabilities.ToDictionary());
            clone.SetCapability("browserstack.user", user);
            return clone;
        }
    }
}