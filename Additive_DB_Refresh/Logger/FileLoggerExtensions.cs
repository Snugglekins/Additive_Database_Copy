using System;
using Microsoft.Extensions.Logging;

namespace Additive_DB_Refresh.Logger
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,string fileName, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(fileName,filePath));
            return factory;
        }
    }
}

