using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.Context;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.ConsoleExtensions.Areas.ConsoleOutput.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.ConsoleCommands
{
    public class ThrowException : IConsoleCommand
    {
        private readonly IConsoleWriter _consoleWriter;
        public string Description { get; } = "Throw Exception";
        public ConsoleKey Key { get; } = ConsoleKey.D3;

        public ThrowException(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public async Task ExecuteAsync()
        {
            var actionResult = await AzureFunctionExecutionContext.ExecuteAsync<ITestService>(
                testService => throw new Exception("Hello Exception!"));

            var objectResult = (ObjectResult)actionResult;
            _consoleWriter.WriteLine(objectResult.Value.ToString(), null, ConsoleColor.Green);
        }
    }
}