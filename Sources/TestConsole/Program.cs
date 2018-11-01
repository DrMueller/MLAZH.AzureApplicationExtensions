using System;
using System.Threading.Tasks;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppInitialization.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.Context;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Settings;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole
{
    public static class Program
    {
        public static void Main()
        {
            var thisAssembly = typeof(Program).Assembly;
            InitializationService.AssureServicesAreInitialized(ContainerConfiguration.CreateFromAssembly(thisAssembly));
            InitializationService.AssureSettingsAreInitialized<AppSettings>("AppSettings", "Development", thisAssembly);

            Task.Run(
                async () =>
                {
                    return await AzureFunctionExecutionContext.ExecuteAsync<ITestService>(
                        service =>
                        {
                            service.DoeSomething();
                            var tra = ServiceLocatorSingleton.Instance.GetService<ITelemetryClientProxy>();
                            tra.TrackEvent("Test 31");
                            tra.TrackEvent("Test 32");
                            tra.TrackEvent("Test 33");
                            throw new Exception("Hello Again 3");
                        });
                });

            Console.ReadKey();
        }
    }
}