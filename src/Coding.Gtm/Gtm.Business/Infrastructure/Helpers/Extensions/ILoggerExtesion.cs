using Microsoft.Extensions.Logging;

namespace Gtm.Business.Infrastructure.Helpers.Extensions
{
    /// <summary>
    /// ILogger extension methods for common scenarios.
    /// </summary>
    public static class ILoggerExtesion
    {
        #region Methods public

        /// <summary>
        /// Checks if the trace log is enabled.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="getMessage">Function to get the message  if log Level is enabled</param>
        public static void TraceIsEnabled(this ILogger logger, Func<string> getMessage)
        {
            CheckLogLevelIsEnabled(logger, LogLevel.Trace, () =>
            {
                logger.LogTrace(getMessage());
            });
        }

        /// <summary>
        /// Checks if the debug log is enabled.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="getMessage">Function to get the message  if log Level is enabled</param>
        public static void DebugIsEnabled(this ILogger logger, Func<string> getMessage)
        {
            CheckLogLevelIsEnabled(logger, LogLevel.Debug, () =>
            {
                logger.LogDebug(getMessage());
            });
        }

        /// <summary>
        /// Checks if the information log is enabled.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="getMessage">Function to get the message  if log Level is enabled</param>
        public static void InformationIsEnabled(this ILogger logger, Func<string> getMessage)
        {
            CheckLogLevelIsEnabled(logger, LogLevel.Information, () =>
            {
                logger.LogInformation(getMessage());
            });
        }

        /// <summary>
        /// Checks if the warning log is enabled.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="getMessage">Function to get the message  if log Level is enabled</param>
        public static void WarningIsEnabled(this ILogger logger, Func<string> getMessage)
        {
            CheckLogLevelIsEnabled(logger, LogLevel.Warning, () =>
            {
                logger.LogWarning(getMessage());
            });
        }

        /// <summary>
        /// Checks if the critical log is enabled.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="getMessage">Function to get the message  if log Level is enabled</param>
        public static void CriticalIsEnabled(this ILogger logger, Func<string> getMessage)
        {
            CheckLogLevelIsEnabled(logger, LogLevel.Critical, () =>
            {
                logger.LogCritical(getMessage());
            });
        }

        /// <summary>
        /// Checks if the error log is enabled.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="getMessage">Function to get the message  if log Level is enabled</param>
        public static void ErrorIsEnabled(this ILogger logger, Func<string> getMessage)
        {
            CheckLogLevelIsEnabled(logger, LogLevel.Error, () =>
            {
                logger.LogError(getMessage());
            });
        }

        /// <summary>
        /// Formats and writes an method begin informational log message.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="methodName">Method name</param>
        public static void LogBeginInformation(this ILogger logger, string methodName)
        {
            logger.LogInformation(string.Concat("Begin: ", methodName));
        }

        /// <summary>
        /// Formats and writes an method end informational log message.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="methodName">Method name</param>
        public static void LogEndInformation(this ILogger logger, string methodName)
        {
            logger.LogInformation(string.Concat("End: ", methodName));
        }

        #endregion

        #region Methods private

        /// <summary>
        /// Checks if the given trace log is enabled.
        /// </summary>
        /// <param name="logger">Represents a type used to perform logging.</param>
        /// <param name="action">Trigger if log Level is enabled</param>
        public static void CheckLogLevelIsEnabled(ILogger logger, LogLevel logLevel, Action action)
        {
            if (logger.IsEnabled(logLevel))
            {
                action();
            }
        }

        #endregion
    }
}
