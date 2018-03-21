using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BrowserStack.Net.Local
{
    internal class BrowserStackTunnel : IBrowserStackTunnel
    {
        private readonly string _binaryName;

        public string[] BasePaths { get; }

        private int _basePathsIndex = -1;
        private string _binaryAbsolute = string.Empty;
        private string _binaryArguments = string.Empty;

        public LocalState LocalState;
        public string LogFilePath = string.Empty;

        private Process _process;

        public void AddBinaryPath(string binaryAbsolute)
        {
            if (binaryAbsolute == null || binaryAbsolute.Trim().Length == 0)
            {
                binaryAbsolute = Path.Combine(BasePaths[++_basePathsIndex], _binaryName);
            }
            _binaryAbsolute = binaryAbsolute;
        }

        public void AddBinaryArguments(string binaryArguments)
        {
            if (binaryArguments == null)
            {
                binaryArguments = "";
            }
            _binaryArguments = binaryArguments;
        }

      
        public BrowserStackTunnel()
        {
            var platformId = Environment.OSVersion.Platform;
            _binaryName = BrowserStackTunnelUtilities.FileNamesByPlatform[platformId];
            LocalState = LocalState.Idle;
            BasePaths = BrowserStackTunnelUtilities.DefaultBasePaths;
        }
        public BrowserStackTunnel(BrowserStackTunnelOptions options) : this()
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            BasePaths = options.BasePaths ?? BrowserStackTunnelUtilities.DefaultBasePaths;
        }

        public void FallbackPaths()
        {
            if (_basePathsIndex >= BasePaths.Length - 1)
            {
                throw new Exception("Binary not found or failed to launch. Make sure that BrowserStackLocal.exe is not already running.");
            }
            _basePathsIndex++;
            _binaryAbsolute = Path.Combine(BasePaths[_basePathsIndex], _binaryName);
        }

        public void Run(string accessKey, string folder, string logFilePath, string processType)
        {
            var arguments = $"-d {processType} ";

            if (folder != null && folder.Trim().Length != 0)
            {
                arguments += $"-f {accessKey} {folder}  {_binaryArguments}";
            }
            else
            {
                arguments += $"{accessKey} {_binaryArguments}";
            }
            if (!File.Exists(_binaryAbsolute))
            {
                throw new InvalidOperationException();
            }

            _process?.Close();

            if (processType.ToLower().Contains("start") && File.Exists(logFilePath))
            {
                File.WriteAllText(logFilePath, string.Empty);
            }
            RunProcess(arguments, processType);
        }

        private void RunProcess(string arguments, string processType)
        {
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = _binaryAbsolute,
                Arguments = arguments,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false
            };

            _process = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            var o = new DataReceivedEventHandler((s, e) =>
            {
                var binaryOutput = JObject.Parse(e.Data ?? "{}");
                if (binaryOutput.GetValue("state") != null && !binaryOutput.GetValue("state").ToString().ToLower().Equals("connected"))
                {
                    throw new Exception($"Eror while executing BrowserStackLocal {processType} {e.Data}");
                }
            });

            _process.OutputDataReceived += o;
            _process.ErrorDataReceived += o;
            _process.Exited += ((s, e) =>
            {
                _process = null;
            });

            _process.Start();

            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();


            AppDomain.CurrentDomain.ProcessExit += (s, e) => Kill();

            _process.WaitForExit();
        }

        public bool IsConnected()
        {
            return LocalState == LocalState.Connected;
        }

        public void Kill()
        {
            if (_process != null)
            {
                _process.Close();
                _process.Kill();
                _process = null;
                LocalState = LocalState.Disconnected;
            }
        }

        public void Dispose()
        {
            if (_process != null)
            {
                Kill();
            }
        }
    }
}