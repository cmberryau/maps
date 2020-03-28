using System;
using log4net.Config;
using Maps.Logging;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using UnityEngine;

namespace Maps.Unity.Logging
{
    /// <summary>
    /// Responsible for correct logging configuration for Maps inside Unity3d
    /// </summary>
    internal static class LoggingConfiguration
    {
        private class UnityAppender : AppenderSkeleton
        {
            /// <inheritdoc />
            protected override void Append(LoggingEvent loggingEvent)
            {
                var message = RenderLoggingEvent(loggingEvent);

                if (Level.Compare(loggingEvent.Level, Level.Error) >= 0)
                {
                    // everything above or equal to error is an error
                    Debug.LogError(message);
                }
                else if (Level.Compare(loggingEvent.Level, Level.Warn) >= 0)
                {
                    // everything that is a warning up to error is logged as warning
                    Debug.LogWarning(message);
                }
                else
                {
                    // everything else we'll just log normally
                    Debug.Log(message);
                }
            }
        }

        /// <summary>
        /// Initializes Logging for the process
        /// </summary>
        public static void Initialize()
        {
            ConfigureConsole();
            ConfigureLog4Net();
        }

        private static void ConfigureConsole()
        {
            Console.SetOut(new ForwardingTextWriter(Debug.Log));
        }

        private static void ConfigureLog4Net()
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date %5level [%thread] (%class:%line) - %message%newline"
            };
            patternLayout.ActivateOptions();

            var unityLogger = new UnityAppender
            {
                Layout = new PatternLayout()
            };
            unityLogger.ActivateOptions();

            BasicConfigurator.Configure(unityLogger);
        }
    }
}