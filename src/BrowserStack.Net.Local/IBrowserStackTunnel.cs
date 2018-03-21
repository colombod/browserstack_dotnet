using System;

namespace BrowserStack.Net.Local
{
    public interface IBrowserStackTunnel : IDisposable
    {
        void AddBinaryPath(string binaryAbsolute);
        void AddBinaryArguments(string binaryArguments);
        void FallbackPaths();
        void Run(string accessKey, string folder, string logFilePath, string processType);
        bool IsConnected();
        void Kill();
        string[] BasePaths { get; }
    }
}