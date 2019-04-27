using System;
using Microsoft.Extensions.Logging;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.Logging.Implementation
{
    internal class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService(ILogger logger)
        {
            _logger = logger;
        }

        public void LogError(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
    }
}