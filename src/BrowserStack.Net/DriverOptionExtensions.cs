using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;

namespace BrowserStack.Net
{
    public static class DriverOptionExtensions
    {
        public static DriverOptions SetupDefaults(this DriverOptions options)
        {
            options
                .SetupOS("Windows", "10")
                .SetResolution(1024, 768)
                .SetupDebug(true, BrowserStackConsoleLogLevel.Info);

            return options;
        }

        public static DriverOptions SetupOS(this DriverOptions options, string osName, string osVersion)
        {

            switch (options)
            {
                case ChromeOptions co:
                    co.AddAdditionalCapability("os", osName, true);
                    co.AddAdditionalCapability("os_version", osVersion, true);
                    break;
                case FirefoxOptions fo:
                    fo.AddAdditionalCapability("os", osName, true);
                    fo.AddAdditionalCapability("os_version", osVersion, true);
                    break;
                case OperaOptions oo:
                    oo.AddAdditionalCapability("os", osName, true);
                    oo.AddAdditionalCapability("os_version", osVersion, true);
                    break;
                case InternetExplorerOptions ieo:
                    ieo.AddAdditionalCapability("os", osName, true);
                    ieo.AddAdditionalCapability("os_version", osVersion, true);
                    break;
                default:
                    options.AddAdditionalCapability("os", osName);
                    options.AddAdditionalCapability("os_version", osVersion);
                    break;
            }

            return options.SetupDebug(true, BrowserStackConsoleLogLevel.Info);
        }


        public static DriverOptions SetupDebug(this DriverOptions options, bool isEnable = true, BrowserStackConsoleLogLevel logLevel = BrowserStackConsoleLogLevel.Errors)
        {
            switch (options)
            {
                case ChromeOptions co:
                    co.AddAdditionalCapability("browserstack.debug", isEnable.ToString().ToLower(), true);
                    co.AddAdditionalCapability("browserstack.console", logLevel.ToString().ToLower(), true);
                    break;
                case FirefoxOptions fo:
                    fo.AddAdditionalCapability("browserstack.debug", isEnable.ToString().ToLower(), true);
                    fo.AddAdditionalCapability("browserstack.console", logLevel.ToString().ToLower(), true);
                    break;
                case OperaOptions oo:
                    oo.AddAdditionalCapability("browserstack.debug", isEnable.ToString().ToLower(), true);
                    oo.AddAdditionalCapability("browserstack.console", logLevel.ToString().ToLower(), true);
                    break;
                case InternetExplorerOptions ieo:
                    ieo.AddAdditionalCapability("browserstack.debug", isEnable.ToString().ToLower(), true);
                    ieo.AddAdditionalCapability("browserstack.console", logLevel.ToString().ToLower(), true);
                    break;
                default:
                    options.AddAdditionalCapability("browserstack.debug", isEnable.ToString().ToLower());
                    options.AddAdditionalCapability("browserstack.console", logLevel.ToString().ToLower());
                    break;
            }
            return options;
        }

        public static DriverOptions SetBrowserVersion(this DriverOptions options, int version)
        {
            options.BrowserVersion = version.ToString("N0");
            return options;
        }

        public static DriverOptions SetupForLocalBridge(this DriverOptions options, string identifier)
        {
            switch (options)
            {
                case ChromeOptions co:
                    co.AddAdditionalCapability("browserstack.local", "true", true);
                    co.AddAdditionalCapability("browserstack.localIdentifier", identifier, true);
                    break;
                case FirefoxOptions fo:
                    fo.AddAdditionalCapability("browserstack.local", "true", true);
                    fo.AddAdditionalCapability("browserstack.localIdentifier", identifier, true);
                    break;
                case OperaOptions oo:
                    oo.AddAdditionalCapability("browserstack.local", "true", true);
                    oo.AddAdditionalCapability("browserstack.localIdentifier", identifier, true);
                    break;
                case InternetExplorerOptions ieo:
                    ieo.AddAdditionalCapability("browserstack.local", "true", true);
                    ieo.AddAdditionalCapability("browserstack.localIdentifier", identifier, true);
                    break;
                default:
                    options.AddAdditionalCapability("browserstack.local", "true");
                    options.AddAdditionalCapability("browserstack.localIdentifier", identifier);
                    break;
            }

            return options;
        }

        public static DriverOptions SetupBrowserStackAuth(this DriverOptions options, string user, string key)
        {
            options
                .SetupUser(user)
                .SetupKey(key);
            return options;
        }

        public static DriverOptions SetupKey(this DriverOptions options, string key)
        {
            switch (options)
            {
                case ChromeOptions co:
                    co.AddAdditionalCapability("browserstack.key", key, true);
                    break;
                case FirefoxOptions fo:
                    fo.AddAdditionalCapability("browserstack.key", key, true);
                    break;
                case OperaOptions oo:
                    oo.AddAdditionalCapability("browserstack.key", key, true);
                    break;
                case InternetExplorerOptions ieo:
                    ieo.AddAdditionalCapability("browserstack.key", key, true);
                    break;
                default:
                    options.AddAdditionalCapability("browserstack.key", key);
                    break;
            }


            return options;
        }

        public static DriverOptions SetupUser(this DriverOptions options, string user)
        {
            switch (options)
            {
                case ChromeOptions co:
                    co.AddAdditionalCapability("browserstack.user", user, true);
                    break;
                case FirefoxOptions fo:
                    fo.AddAdditionalCapability("browserstack.user", user, true);
                    break;
                case OperaOptions oo:
                    oo.AddAdditionalCapability("browserstack.user", user, true);
                    break;
                case InternetExplorerOptions ieo:
                    ieo.AddAdditionalCapability("browserstack.user", user, true);
                    break;
                default:
                    options.AddAdditionalCapability("browserstack.user", user);
                    break;
            }

            return options;
        }

        public static DriverOptions SetResolution(this DriverOptions options, int width, int height)
        {
            switch (options)
            {
                case ChromeOptions co:
                    co.AddAdditionalCapability("resolution", $"{width}x{height}", true);
                    break;
                case FirefoxOptions fo:
                    fo.AddAdditionalCapability("resolution", $"{width}x{height}", true);
                    break;
                case OperaOptions oo:
                    oo.AddAdditionalCapability("resolution", $"{width}x{height}", true);
                    break;
                case InternetExplorerOptions ieo:
                    ieo.AddAdditionalCapability("resolution", $"{width}x{height}", true);
                    break;
                default:
                    options.AddAdditionalCapability("resolution", $"{width}x{height}");
                    break;
            }

            return options;
        }

        public static DriverOptions SetupTestDetails(this DriverOptions options, string project = "", string build = "", string label = "", [CallerMemberName]string name = null)
        {
            var testName = name ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(label))
            {
                testName = string.IsNullOrWhiteSpace(testName) ? label : $"{testName} [{label}]";
            }

            switch (options)
            {
                case ChromeOptions co:
                    co.AddAdditionalCapability("project", project ?? string.Empty, true);
                    co.AddAdditionalCapability("build", build ?? string.Empty, true);
                    co.AddAdditionalCapability("name", testName, true);
                    break;
                case FirefoxOptions fo:
                    fo.AddAdditionalCapability("project", project ?? string.Empty, true);
                    fo.AddAdditionalCapability("build", build ?? string.Empty, true);
                    fo.AddAdditionalCapability("name", testName, true);
                    break;
                case OperaOptions oo:
                    oo.AddAdditionalCapability("project", project ?? string.Empty, true);
                    oo.AddAdditionalCapability("build", build ?? string.Empty, true);
                    oo.AddAdditionalCapability("name", testName, true);
                    break;
                case InternetExplorerOptions ieo:
                    ieo.AddAdditionalCapability("project", project ?? string.Empty, true);
                    ieo.AddAdditionalCapability("build", build ?? string.Empty, true);
                    ieo.AddAdditionalCapability("name", testName, true);
                    break;
                default:
                    options.AddAdditionalCapability("project", project ?? string.Empty);
                    options.AddAdditionalCapability("build", build ?? string.Empty);
                    options.AddAdditionalCapability("name", testName);
                    break;
            }
            return options;
        }
    }
}