using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctionExecution.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole
{
    public static class Program
    {
        public static void Main()
        {
            var thisAssembly = typeof(Program).Assembly;
            Task.Run(
                async () =>
                {
                    return await AzureFunctionExecutionContext.ExecuteAsync<ITestService>(
                        service =>
                        {
                            service.DoeSomething();
                            throw new Exception("Hello Test");
                        },
                        thisAssembly);
                });

            Console.ReadKey();
        }
    }
}