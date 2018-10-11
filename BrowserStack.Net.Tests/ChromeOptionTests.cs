using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BrowserStack.Net.Tests
{
    public class ChromeOptionTests
    {
        [Fact]
        public void When_creating_options_for_chrome_they_are_of_the_right_type()
        {
            var options = DriverOptionFactory.SetupChrome();
            options.Should().BeAssignableTo<ChromeOptions>();
        }

       
    }
}

