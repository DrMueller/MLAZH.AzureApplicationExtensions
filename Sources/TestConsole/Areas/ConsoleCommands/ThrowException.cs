using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.ConsoleExtensions.Areas.ConsoleOutput.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.ConsoleCommands
{
    public class ThrowException : IConsoleCommand
    {
        private readonly IAzureFunctionContext _azureFunctionContext;
        private readonly IConsoleWriter _consoleWriter;
        public string Description { get; } = "Throw Exception";
        public ConsoleKey Key { get; } = ConsoleKey.D3;

        public ThrowException(IConsoleWriter consoleWriter, IAzureFunctionContext azureFunctionContext)
        {
            _consoleWriter = consoleWriter;
            _azureFunctionContext = azureFunctionContext;
        }

        public async Task ExecuteAsync()
        {
            var httpRequest = new DefaultHttpRequest(new DefaultHttpContext());

            var actionResult = await _azureFunctionContext.ExecuteHttpRequestAsync(
                httpRequest,
                _ => throw new Exception("Hello Exception!"));

            var objectResult = (ObjectResult)actionResult;
            _consoleWriter.WriteLine(objectResult.Value.ToString(), null, ConsoleColor.Green);
        }
    }
}