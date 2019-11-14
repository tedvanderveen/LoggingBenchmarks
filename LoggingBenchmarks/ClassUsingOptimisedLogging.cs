﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace LoggingBenchmarks
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ClassUsingOptimisedLogging : ILogging
    {
        private readonly ILogger _logger;

        public ClassUsingOptimisedLogging(ILogger logger) => _logger = logger;

        public void LogOnceNoParams() => Log.InformationalMessageNoParams(_logger);

        public void LogOnceOneParam(string value1) => Log.InformationalMessageOneParam(_logger, value1);

        public void LogOnceOneParam(int value1) => Log.InformationalMessageOneParam(_logger, value1);

        public void LogOnceTwoParams(string value1, int value2) => Log.InformationalMessageTwoParams(_logger, value1, value2);

        public void LogOnceUsingLoggerMessageWithoutLevelCheck(string value1, int value2) => Log.DebugMessage(_logger, value1, value2);

        public void LogOnceUsingLoggerMessageWithLevelCheck(string value1, int value2) => Log.DebugMessageWithLevelCheck(_logger, value1, value2);

        internal static class Events
        {
            public static readonly EventId Started = new EventId(100, "Started");
        }

        private static class Log
        {
            private static readonly Action<ILogger, Exception> _informationLoggerMessageNoParams = LoggerMessage.Define(
                LogLevel.Information,
                Events.Started,
                "This is a message with no params!");

            private static readonly Action<ILogger, string, Exception> _informationLoggerMessageOneParam = LoggerMessage.Define<string>(
                LogLevel.Information,
                Events.Started,
                "This is a message with one param! {Param1}");

            private static readonly Action<ILogger, int, Exception> _informationLoggerMessageOneIntParam = LoggerMessage.Define<int>(
                LogLevel.Information,
                Events.Started,
                "This is a message with one param! {Param1}");

            private static readonly Action<ILogger, string, int, Exception> _informationLoggerMessage = LoggerMessage.Define<string, int>(
                LogLevel.Information,
                Events.Started,
                "This is a message with two params! {Param1}, {Param2}");
            
            private static readonly Action<ILogger, string, int, Exception> _debugLoggerMessage = LoggerMessage.Define<string, int>(
                LogLevel.Debug,
                Events.Started,
                "This is a debug message with two params! {Param1}, {Param2}");

            public static void InformationalMessageNoParams(ILogger logger) => _informationLoggerMessageNoParams(logger, null);

            public static void InformationalMessageOneParam(ILogger logger, string value1) => _informationLoggerMessageOneParam(logger, value1, null);

            public static void InformationalMessageOneParam(ILogger logger, int value1) => _informationLoggerMessageOneIntParam(logger, value1, null);

            public static void InformationalMessageTwoParams(ILogger logger, string value1, int value2) => _informationLoggerMessage(logger, value1, value2, null);

            public static void DebugMessage(ILogger logger, string value1, int value2) => _debugLoggerMessage(logger, value1, value2, null);

            public static void DebugMessageWithLevelCheck(ILogger logger, string value1, int value2)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                    _debugLoggerMessage(logger, value1, value2, null);
            }
        }
    }
}