using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

namespace BrowserStack.Net
{
    public static class  DriverOptionFactory{

        public static DriverOptions Setup(BrowserStackSupportedBrowsers browser)
        {
            DriverOptions options;
            switch (browser)
            {
                case BrowserStackSupportedBrowsers.firefox:
                    options = SetupFireFox();
                    break;
                case BrowserStackSupportedBrowsers.chrome:
                    options = SetupChrome();
                    break;
                case BrowserStackSupportedBrowsers.internet_explorer:
                    options = SetupInternetExplorer();
                    break;
                case BrowserStackSupportedBrowsers.safari:
                    options = SetupSafari();
                    break;
                case BrowserStackSupportedBrowsers.opera:
                    options = SetupOpera();
                    break;
                case BrowserStackSupportedBrowsers.edge:
                    options = SetupEdge();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }

            return options;
        }

        public static DriverOptions SetupSafari()
        {
           return new SafariOptions();
        }

        public static DriverOptions SetupChrome(bool enableMixedContent = true)
        {
            var chromeChromeOptions = new ChromeOptions();

            if (enableMixedContent)
            {
                // disable chrome security

                chromeChromeOptions.AddArgument("--disable-web-security");
                chromeChromeOptions.AddArgument("--allow-running-insecure-content");
                chromeChromeOptions.AcceptInsecureCertificates = true;
            }

            return chromeChromeOptions;
        }

        public static DriverOptions SetupEdge()
        {
            return new EdgeOptions();
        }

        public static DriverOptions SetupInternetExplorer()
        {
            return new InternetExplorerOptions();
        }

        public static DriverOptions SetupFireFox()
        {
            return new FirefoxOptions();
        }

        public static DriverOptions SetupOpera()
        {
            return new OperaOptions();
        }
    }
}