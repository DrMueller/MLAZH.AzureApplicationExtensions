using System;
using Mmu.Mlh.ConsoleExtensions.Areas.ConsoleOutput.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.Services.Implementation
{
    public class TestService : ITestService
    {
        private readonly IConsoleWriter _consoleWriter;

        public TestService(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public void DoSomething()
        {
            _consoleWriter.WriteLine(
                $"{nameof(TestService)} did something!",
                null,
                ConsoleColor.Green);
        }
    }
}