using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace BrowserStack.Net.Local
{
    public class BrowserStackTunnelDownloader : IBrowserStackTunnelDownloader
    {
        private readonly Func<IWebDownloader> _clientFactory;
        private readonly PlatformID _currentPlatformId = Environment.OSVersion.Platform;

        [ExcludeFromCodeCoverage]
        public BrowserStackTunnelDownloader()
        {
            _clientFactory = () => new WebClientImpl();
        }

        public BrowserStackTunnelDownloader(Func<IWebDownloader> clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task DownloadTunnelBinariesAsync(string destinationPath)
        {
            var absolutedestinationPath = Path.GetFullPath(destinationPath);

            if (!Directory.Exists(absolutedestinationPath))
            {
                Directory.CreateDirectory(absolutedestinationPath);
            }

            foreach (var entry in BrowserStackTunnelUtilities.FileNamesByPlatform)
            {
                await Download(absolutedestinationPath, entry.Key, $"{BrowserStackTunnelUtilities.DownloadUrlsByPlatform[entry.Key]}{entry.Value}", entry.Value);
            }
        }

        private async Task Download(string root, PlatformID platformId, string downloadUrl, string fileName)
        {
            var destination = Path.Combine(root, platformId.ToString());
            var binaryAbsolute = Path.Combine(destination, fileName);
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            using (var client = _clientFactory())
            {
                await client.DownloadFileTaskAsync(downloadUrl, binaryAbsolute);
            }

            if (_currentPlatformId == platformId && !File.Exists(binaryAbsolute))
            {
                throw new InvalidOperationException($"Error accessing file {binaryAbsolute}");
            }
        }
    }
}