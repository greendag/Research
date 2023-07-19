using System;

namespace Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger(string categoryName, LoggerType loggerType)
        {
            switch (loggerType)
            {
                case LoggerType.ConsoleLogger:
                    return new ConsoleLogger(categoryName);

                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}