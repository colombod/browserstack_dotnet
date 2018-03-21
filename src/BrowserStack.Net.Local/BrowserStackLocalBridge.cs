using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BrowserStack.Net.Local
{
    public class BrowserStackLocalBridge : IDisposable
    {
        private string _folder = string.Empty;
        private string _accessKey = string.Empty;
        private string _customLogPath = string.Empty;
        private string _argumentString = string.Empty;
        private string _customBinaryPath = string.Empty;
        private readonly IBrowserStackTunnel _tunnel;

        private static readonly Dictionary<string, string> ValueCommands =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"localIdentifier", "-localIdentifier"},
                {"hosts", ""},
                {"proxyHost", "-proxyHost"},
                {"proxyPort", "-proxyPort"},
                {"proxyUser", "-proxyUser"},
                {"proxyPass", "-proxyPass"}
            };

        private static readonly Dictionary<string, string> BooleanCommands =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "v", "-vvv"},
                { "force", "-force"},
                { "forcelocal", "-forcelocal"},
                { "forceproxy", "-forceproxy"},
                { "onlyAutomate", "-onlyAutomate"}
            };

        public bool IsRunning()
        {
            return _tunnel?.IsConnected() == true;
        }

        private void AddArgs(string key, string value)
        {
            key = key.Trim();
            switch (key)
            {
                case "key":
                    _accessKey = value;
                    break;
                case "f":
                    _folder = value;
                    break;
                case "binarypath":
                    _customBinaryPath = value;
                    break;
                case "logfile":
                    _customLogPath = value;
                    break;
                default:
                    {
                        if (ValueCommands.TryGetValue(key, out var valueCommand))
                        {
                            _argumentString += $"{valueCommand} {value} ";
                            
                        }else  if (BooleanCommands.TryGetValue(key, out var booleanCommand))
                        {
                            if (value.Trim().ToLower() == "true")
                            {
                                _argumentString += $"{booleanCommand} ";
                            }
                        }
                        else
                        {
                            if (value.Trim().ToLower() == "true")
                            {
                                _argumentString += $"-{key} ";
                            }
                            else
                            {
                                _argumentString += $"-{key} {value} ";
                            }
                        }
                    }
                    break;
            }

        }

        public BrowserStackLocalBridge()
        {
            _tunnel = new BrowserStackTunnel();
        }

        public BrowserStackLocalBridge(BrowserStackTunnelOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _tunnel = new BrowserStackTunnel(options);
        }

        public BrowserStackLocalBridge(IBrowserStackTunnel tunnel)
        {
            _tunnel = tunnel;
        }

        public void Start(params (string key, string value)[] options)
        {
            foreach (var (key, value) in options)
            {
                AddArgs(key, value);
            }

            if (string.IsNullOrWhiteSpace(_accessKey))
            {
                _accessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
                if (string.IsNullOrWhiteSpace(_accessKey))
                {
                    throw new InvalidOperationException("BROWSERSTACK_ACCESS_KEY cannot be empty. Specify one by adding key to options or adding to the environment variable BROWSERSTACK_ACCESS_KEY.");
                }
                Regex.Replace(_accessKey, @"\s+", string.Empty);
            }

            if (_customLogPath == null || _customLogPath.Trim().Length == 0)
            {
                _customLogPath = Path.Combine(_tunnel.BasePaths[1], "local.log");
            }

            _argumentString += $"-logFile \"{_customLogPath}\" ";
            _tunnel.AddBinaryPath(_customBinaryPath);
            _tunnel.AddBinaryArguments(_argumentString);
            while (true)
            {
                var except = false;
                try
                {
                    _tunnel.Run(_accessKey, _folder, _customLogPath, "start");
                }
                catch (Exception)
                {
                    except = true;
                }
                if (except)
                {
                    _tunnel.FallbackPaths();
                }
                else
                {
                    break;
                }
            }
        }

        public void Stop()
        {
            _tunnel.Run(_accessKey, _folder, _customLogPath, "stop");
            _tunnel.Kill();
        }

        public void Dispose()
        {
            _tunnel?.Dispose();
        }
    }
}