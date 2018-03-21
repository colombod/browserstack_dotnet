namespace BrowserStack.Net.Local
{
    public class BrowserStackTunnelOptions
    {
        public BrowserStackTunnelOptions(IBrowserStackTunnelDownloader downloader = null, string[] basePaths = null)
        {
            Downloader = downloader;
            BasePaths = basePaths;
        }

        public IBrowserStackTunnelDownloader Downloader { get;  }
        public string[] BasePaths { get;  }
    }
}