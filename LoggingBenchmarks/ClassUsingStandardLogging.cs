﻿using Microsoft.Extensions.Logging;

namespace LoggingBenchmarks
{
    public class ClassUsingStandardLogging : ILogging
    {
        private readonly ILogger _logger;

        public ClassUsingStandardLogging(ILogger logger) => _logger = logger;

        public void LogOnceNoParams() => 
            _logger.LogInformation("This is a message with no params!");

        public void LogOnceOneParam(string value1) => 
            _logger.LogInformation("This is a message with one param! {Param1}", value1);

        public void LogOnceOneParam(int value1) =>
            _logger.LogInformation("This is a message with one param! {Param1}", value1);

        public void LogOnceTwoParams(string value1, int value2) => 
            _logger.LogInformation("This is a message with two params! {Param1}, {Param2}", value1, value2);

        public void LogDebugOnceWithTwoParams(string value1, int value2) => 
            _logger.LogDebug("This is a debug message with two params! {Param1}, {Param2}", value1, value2);

    }
}