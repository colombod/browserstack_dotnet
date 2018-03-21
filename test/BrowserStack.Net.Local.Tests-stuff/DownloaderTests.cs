using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;

namespace BrowserStack.Net.Local.Tests
{
    public class DownloaderTests
    {
        [Fact]
        public async Task it_downloads_for_all_platforms()
        {
            var downloaded = new List<(string url, string file)>();
            var expected = new List<(string, string)>();
            foreach (var entry in BrowserStackTunnelUtilities.DownloadUrlsByPlatform)
            {
                var url = $"{entry.Value}{BrowserStackTunnelUtilities.FileNamesByPlatform[entry.Key]}";
                var file = $"\\{Path.Combine(entry.Key.ToString(), BrowserStackTunnelUtilities.FileNamesByPlatform[entry.Key])}";
                expected.Add((url,file));
            }
            var root = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
           
            var mock = new Mock<IWebDownloader>();
            mock.Setup(c => c.DownloadFileTaskAsync(It.IsAny<string>(), It.IsAny<string>())).Returns<string,string>((url,file ) =>
            {
                downloaded.Add((url,file.Replace(root, string.Empty)));
                File.WriteAllBytes(file,Array.Empty<byte>());
                return Task.CompletedTask;
                
            });
            var downlaoder = new BrowserStackTunnelDownloader(() => mock.Object);
            await downlaoder.DownloadTunnelBinariesAsync(root);

            Directory.Exists(root).Should().BeTrue();
            downloaded.Should().BeEquivalentTo(expected);
            Directory.Delete(root, true);
        }

        [Fact]
        public void it_fails_if_cannot_downloadForCurrentPlatform()
        {
            var root = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()));

            var mock = new Mock<IWebDownloader>();
            mock.Setup(c => c.DownloadFileTaskAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var downlaoder = new BrowserStackTunnelDownloader(() => mock.Object);
         
            Action action = () =>
            {
                downlaoder.DownloadTunnelBinariesAsync(root).Wait();
            };
            action.Should().Throw<InvalidOperationException>();
            Directory.Delete(root, true);
        }
    }
}


