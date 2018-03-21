using System;
using System.Threading.Tasks;

namespace BrowserStack.Net.Local
{
    public interface IWebDownloader : IDisposable
    {
        
        Task DownloadFileTaskAsync(string address, string fileName);
        Task DownloadFileTaskAsync(Uri address, string fileName);
    }
}