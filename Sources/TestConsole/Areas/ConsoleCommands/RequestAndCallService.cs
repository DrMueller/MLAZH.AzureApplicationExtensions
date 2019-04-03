using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.ConsoleCommands
{
    public class RequestAndCallService : IConsoleCommand
    {
        private readonly IAzureFunctionContext _azureFunctionContext;
        public string Description { get; } = "Request and call Service";
        public ConsoleKey Key { get; } = ConsoleKey.D1;

        public RequestAndCallService(IAzureFunctionContext azureFunctionContext)
        {
            _azureFunctionContext = azureFunctionContext;
        }

        public async Task ExecuteAsync()
        {
            await _azureFunctionContext.ExecuteActionAsync(
                serviceLocator =>
                {
                    var testService = serviceLocator.GetService<ITestService>();
                    testService.DoSomething();
                    var result = (IActionResult)new OkResult();
                    return Task.FromResult(result);
                });
        }
    }
}