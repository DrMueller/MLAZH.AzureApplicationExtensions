using System;
using System.Threading.Tasks;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppInitialization.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctionExecution;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Settings;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole
{
    public static class Program
    {
        public static void Main()
        {
            var thisAssembly = typeof(Program).Assembly;
            InitializationService.AssureServicesAreInitialized(ContainerConfiguration.CreateFromAssembly(thisAssembly));
            InitializationService.AssureSettingsAreInitialized<AppSettings>("AppSettings", thisAssembly);

            Task.Run(
                async () =>
                {
                    return await AzureFunctionExecutionContext.ExecuteAsync<ITestService>(
                        service =>
                        {
                            service.DoeSomething();
                            throw new Exception("Hello Again");
                        });
                });

            Console.ReadKey();
        }
    }
}