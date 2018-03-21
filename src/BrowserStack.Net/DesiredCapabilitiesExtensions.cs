using System.Runtime.CompilerServices;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace BrowserStack.Net
{
    public static class DesiredCapabilitiesExtensions
    {
        public static DesiredCapabilities SetupDefaults(this DesiredCapabilities capabilities)
        {
            capabilities.SetCapability("os", "Windows");
            capabilities.SetCapability("os_version", "10");
            capabilities.SetCapability("resolution", "1024x768");

            return capabilities.SetupDedug(true, BrowserStackConsoleLogLevel.Info);
        }

        public static DesiredCapabilities SetupTestDetails(this DesiredCapabilities capabilities, string project = "", string build = "", [CallerMemberName]string name = null)
        {
            
            capabilities.SetCapability("project", project?? string.Empty);
            capabilities.SetCapability("build", build ?? string.Empty);
            capabilities.SetCapability("name", name ?? string.Empty);
            return capabilities;
        }

        public static DesiredCapabilities SetupDedug(this DesiredCapabilities capabilities, bool isEnable = false, BrowserStackConsoleLogLevel logLevel = BrowserStackConsoleLogLevel.Errors)
        {
            capabilities.SetCapability("browserstack.debug", isEnable.ToString().ToLower());
            capabilities.SetCapability("browserstack.console", logLevel.ToString().ToLower());
            return capabilities;
        }

        public static DesiredCapabilities SetBrowserVersion(this DesiredCapabilities capabilities, int version)
        {
            capabilities.SetCapability("browser_version", version.ToString("N0"));
            return capabilities;
        }

        public static DesiredCapabilities SetBrowser(this DesiredCapabilities capabilities, BrowserStackSupportedBrowsers browser)
        {
            capabilities.SetCapability("browser", browser.ToString().Replace("_", " "));
            return capabilities;
        }

        public static DesiredCapabilities SetupChrome(this DesiredCapabilities capabilities, int version = 64, bool enableMixedContent = false)
        {
            capabilities
                .SetBrowser(BrowserStackSupportedBrowsers.chrome)
                .SetBrowserVersion(version);

            if (enableMixedContent)
            {
                // disable chrome security
                var options = new ChromeOptions();

                options.AddArgument("--disable-web-security");
                options.AddArgument("--allow-running-insecure-content");
                capabilities.SetCapability(ChromeOptions.Capability, options);
            }

            return capabilities;

        }

        public static DesiredCapabilities SetupEdge(this DesiredCapabilities capabilities, int version = 16)
        {
           return  capabilities
                .SetBrowser(BrowserStackSupportedBrowsers.edge)
                .SetBrowserVersion(version);
        }

        public static DesiredCapabilities SetupForLocalBridge(this DesiredCapabilities capabilities, string identifier)
        {
            capabilities.SetCapability("browserstack.local", "true");
            capabilities.SetCapability("browserstack.localIdentifier", identifier);
            return capabilities;
        }

        public static DesiredCapabilities SetupKey(this DesiredCapabilities capabilities, string key)
        {
            capabilities.SetCapability("browserstack.key", key);
            return capabilities;
        }

        public static DesiredCapabilities SetupUser(this DesiredCapabilities capabilities, string user)
        {
            capabilities.SetCapability("browserstack.user", user);
            return capabilities;
        }
    }
}