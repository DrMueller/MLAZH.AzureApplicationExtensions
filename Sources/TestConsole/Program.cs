using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppInitialization.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Settings;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Services;
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

            ServiceLocatorSingleton
                .Instance
                .GetService<IConsoleCommandsStartupService>()
                .Start();
        }
    }
}