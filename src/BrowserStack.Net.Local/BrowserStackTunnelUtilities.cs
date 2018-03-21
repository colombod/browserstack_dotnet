using System;
using System.Collections.Generic;
using System.IO;

namespace BrowserStack.Net.Local
{
    public static class BrowserStackTunnelUtilities
    {
        public static readonly Dictionary<PlatformID, string> FileNamesByPlatform = new Dictionary<PlatformID, string>
        {
            {PlatformID.MacOSX, "BrowserStackLocal-darwin-x64"},
            {PlatformID.Win32NT, "BrowserStackLocal.exe"},
            {PlatformID.Unix, "BrowserStackLocal-linux-x64"}
        };

        public static readonly string[] DefaultBasePaths = {
            Path.Combine(Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"), ".browserstack"),
            Directory.GetCurrentDirectory(),
            Path.GetTempPath()
        };

        public static readonly Dictionary<PlatformID, string> DownloadUrlsByPlatform = new Dictionary<PlatformID, string>
        {
            {PlatformID.MacOSX, "https://s3.amazonaws.com/browserStack/browserstack-local/"},
            {PlatformID.Win32NT, "https://s3.amazonaws.com/browserStack/browserstack-local/"},
            {PlatformID.Unix, "https://s3.amazonaws.com/browserStack/browserstack-local/"}
        };
    }
}