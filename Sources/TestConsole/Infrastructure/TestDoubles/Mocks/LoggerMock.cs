using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Infrastructure.TestDoubles.Mocks
{
    public class LoggerMock : ILogger
    {
        private List<string> _receivedLogMessages = new List<string>();
        public IReadOnlyCollection<string> ReceivedLogMessages => _receivedLogMessages;

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _receivedLogMessages.Add(state.ToString());
        }
    }
}

