using System.Threading.Tasks;

namespace BrowserStack.Net.Local
{
    public interface IBrowserStackTunnelDownloader
    {
        Task DownloadTunnelBinariesAsync(string destinationPath);

    }
}