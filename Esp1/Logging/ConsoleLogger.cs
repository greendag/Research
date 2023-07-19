using System;
using System.Reflection;

namespace Logging
{
    /// <summary>A logger that prints to the debug console</summary>
    public class ConsoleLogger : ILogger
    {
        #region Properties

        /// <summary>Name of the logger</summary>
        public string LoggerName { get; }

        /// <summary>Sets the minimum log level</summary>
        public LogLevel MinLogLevel { get; set; }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel) => logLevel >= MinLogLevel;

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new instance of the <see cref="ConsoleLogger" />
        /// </summary>
        /// <param name="loggerName">The logger name</param>
        public ConsoleLogger(string loggerName)
        {
            LoggerName = loggerName;
            MinLogLevel = LogLevel.Debug;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public void Log(LogLevel logLevel, EventId eventId, string state, Exception exception, MethodInfo format)
        {
            if (logLevel < MinLogLevel)
            {
                return;
            }

            string str1;
            if (format == null)
            {
                str1 = exception == null ? state : $"{state} {exception}";
            }
            else
            {
                str1 = (string)format.Invoke(null, new object[]
                {
                    LoggerName,
                    logLevel,
                    eventId,
                    state,
                    exception
                });
            }

            Console.WriteLine(str1);
        }

        #endregion
    }
}
