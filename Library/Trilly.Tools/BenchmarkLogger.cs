using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Serilog;

namespace Trilly.Tools
{
    public class BenchmarkLogger : IDisposable
    {
        private readonly Stopwatch _timer = new Stopwatch();
        private readonly string _benchmarkName;
        private readonly ILogger _logger;

        public BenchmarkLogger(string benchmarkName, ILogger logger)
        {
            _benchmarkName = benchmarkName;
            _logger = logger;
            _timer.Start();
        }

        public void Dispose()
        {
            _timer.Stop();

            var timerInfo = $"Czas realizacji {_benchmarkName}: {_timer.Elapsed.ToString()}";

            Console.WriteLine(timerInfo);
            if (_logger != null)
            {
                _logger.Information(timerInfo);
            }
            else
            {
                Debug.WriteLine(timerInfo);
            }
        }
    }
}
