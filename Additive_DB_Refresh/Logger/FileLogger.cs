using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Additive_DB_Refresh.Logger
{
	public class FileLogger : ILogger
	{
        private string filePath;
        private string fullFilePath;
        private static object _lock = new object();
        public FileLogger(string fileName, string path)
        {
            filePath = path;
            //string fileName = String.Format("{0}_{1}.log",filePrefix, DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"));
            fullFilePath = Path.Combine(filePath, fileName);
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    //DateTime.UtcNow,
                    var n = Environment.NewLine;
                    string exc = "";
                    if (exception != null) exc = n + exception.GetType() + ": " + exception.Message + n + exception.StackTrace + n;
                    File.AppendAllText(fullFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + logLevel.ToString() + ": " +  " " + formatter(state, exception) + n + exc);
                }
            }
        }
    }
}

