﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;

namespace BrowserStack.Net
{
    public static class DriverExtensions
    {
        public static void PerformKeyStrokes(this IWebDriver driver, string keys)
        {
            var a = new Actions(driver);
            a.SendKeys(keys).Perform();
        }

        public static void SetFocus(this IWebDriver driver, IWebElement element)
        {
            var a = new Actions(driver);
            a.MoveToElement(element).Click().Perform();
        }
      
        public static void PostMessageToIFrame(this IWebDriver driver, int frameIndex, string origin, string message)
        {
            driver.ExecuteJavaScript($"document.getElementsByTagName('iframe')[{frameIndex}].contentWindow.postMessage({message}, \"{origin}\");");
        }

        public static void PostMessageToIFrame(this IWebDriver driver, int frameIndex, string origin, JObject message)
        {
            driver.PostMessageToIFrame(frameIndex, origin, message.ToString(Formatting.None));
        }

        public static void PostMessageToIFrame<T>(this IWebDriver driver, int frameIndex, string origin, T message)
        {
            driver.PostMessageToIFrame(frameIndex, origin, JsonConvert.SerializeObject(message));
        }

        public static void PostMessageToIFrame(this IWebDriver driver, int frameIndex, Uri origin, string message)
        {
            driver.PostMessageToIFrame(frameIndex, origin.ToString(), message);
        }

        public static void PostMessageToIFrame(this IWebDriver driver, int frameIndex, Uri origin, JObject message)
        {
            driver.PostMessageToIFrame(frameIndex, origin, message.ToString(Formatting.None));
        }

        public static void PostMessageToIFrame<T>(this IWebDriver driver, int frameIndex, Uri origin, T message)
        {
            driver.PostMessageToIFrame(frameIndex, origin, JsonConvert.SerializeObject(message));
        }
    }
}