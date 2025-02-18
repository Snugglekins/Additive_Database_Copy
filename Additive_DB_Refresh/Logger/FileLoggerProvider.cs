using System;
using Microsoft.Extensions.Logging;

namespace Additive_DB_Refresh.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string path;
        private string fileName;
        public FileLoggerProvider(string _filePrefix, string _path)
        {
            path = _path;
            fileName = String.Format("{0}_{1}.log",_filePrefix, DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"));

        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(fileName,path);
        }

        public void Dispose()
        {
        }
    }
}

