using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.Context;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.ConsoleCommands
{
    public class RequestAndCallService : IConsoleCommand
    {
        public string Description { get; } = "Request and call Service";
        public ConsoleKey Key { get; } = ConsoleKey.D1;

        public async Task ExecuteAsync()
        {
            await AzureFunctionExecutionContext.ExecuteAsync<ITestService>(
                testService =>
                {
                    testService.DoSomething();
                    var result = (IActionResult)new OkResult();
                    return Task.FromResult(result);
                });
        }
    }
}