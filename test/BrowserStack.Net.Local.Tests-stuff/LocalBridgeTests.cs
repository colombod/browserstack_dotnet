using System;
using System.IO;
using Moq;
using Xunit;
using FluentAssertions;

namespace BrowserStack.Net.Local.Tests
{
    public class LocalBridgeTests : IDisposable
    {
        private readonly string _logAbsolute = Path.Combine(Directory.GetCurrentDirectory(), "local.log");

        public LocalBridgeTests()
        {
            Environment.SetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY", null);
        }

        [Fact]
        public void bridge_requires_key()
        {
            var local = new BrowserStackLocalBridge();
            Action action = () => local.Start();
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void bridge_can_get_key_from_env()
        {
            Environment.SetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY", "envDummyKey");
            var local = new BrowserStackLocalBridge();
            Action action = () => local.Start();
            action.Should().NotThrow<InvalidOperationException>();
        }

        [Fact]
        public void bridge_can_get_key_from_parameters()
        {
            var local = new BrowserStackLocalBridge();
            Action action = () => local.Start(("key", "paramsDummyKey"));
            action.Should().NotThrow<InvalidOperationException>();
        }

        [Fact]
        public void bridge_forwards_custom_parameters()
        {
            var tunnelMock = new Mock<IBrowserStackTunnel>();
            tunnelMock.Setup(mock => mock.Run("dummyKey", string.Empty, _logAbsolute, "start"));
            tunnelMock.SetupGet(mock => mock.BasePaths).Returns(BrowserStackTunnelUtilities.DefaultBasePaths);
            var local = new BrowserStackLocalBridge(tunnelMock.Object);


            local.Start(("key", "dummyKey"), ("customBoolKey1", "true"), ("customBoolKey2", "false"), ("customKey1", "customValue1"), ("customKey2", "customValue2"));
            tunnelMock.Verify(mock => mock.AddBinaryPath(string.Empty), Times.Once);
            tunnelMock.Verify(mock => mock.AddBinaryArguments(
                It.IsRegex("-customBoolKey1.*-customBoolKey2.*-customKey1.*customValue1.*-customKey2.*customValue2")
            ), Times.Once());
            tunnelMock.Verify(mock => mock.Run("dummyKey", string.Empty, _logAbsolute, "start"), Times.Once());
        }

        [Fact]
        public void bridge_forwards_boolean_parameters()
        {
            var tunnelMock = new Mock<IBrowserStackTunnel>();
            tunnelMock.Setup(mock => mock.Run("dummyKey", string.Empty, _logAbsolute, "start"));
            tunnelMock.SetupGet(mock => mock.BasePaths).Returns(BrowserStackTunnelUtilities.DefaultBasePaths);
            var local = new BrowserStackLocalBridge(tunnelMock.Object);
            local.Start(("key", "dummyKey"), ("v", "true"), ("force", "true"), ("forcelocal", "true"), ("forceproxy", "true"), ("onlyAutomate", "true"));
            tunnelMock.Verify(mock => mock.AddBinaryPath(string.Empty), Times.Once);
            tunnelMock.Verify(mock => mock.AddBinaryArguments(It.IsRegex("-vvv.*-force.*-forcelocal.*-forceproxy.*-onlyAutomate")), Times.Once());
            tunnelMock.Verify(mock => mock.Run("dummyKey", string.Empty, _logAbsolute, "start"), Times.Once());
        }

        [Fact]
        public void bridge_forwards_value_parameters()
        {
            var tunnelMock = new Mock<IBrowserStackTunnel>();
            tunnelMock.Setup(mock => mock.Run("dummyKey", string.Empty, _logAbsolute, "start"));
            tunnelMock.SetupGet(mock => mock.BasePaths).Returns(BrowserStackTunnelUtilities.DefaultBasePaths);
            var local = new BrowserStackLocalBridge(tunnelMock.Object);
            local.Start(("key", "dummyKey"), ("localIdentifier", "dummyIdentifier"), ("hosts", "dummyHost"), ("proxyHost", "dummyHost"), ("proxyPort", "dummyPort"), ("proxyUser", "dummyUser"),("proxyPass", "dummyPass"));
            tunnelMock.Verify(mock => mock.AddBinaryPath(string.Empty), Times.Once);
            tunnelMock.Verify(mock => mock.AddBinaryArguments(
                It.IsRegex("-localIdentifier.*dummyIdentifier.*dummyHost.*-proxyHost.*dummyHost.*-proxyPort.*dummyPort.*-proxyUser.*dummyUser.*-proxyPass.*dummyPass")
            ), Times.Once());
            tunnelMock.Verify(mock => mock.Run("dummyKey", string.Empty, _logAbsolute, "start"), Times.Once());
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY", null);
        }
    }
}
