using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppInitialization.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Infrastructure.ExecutionContexts.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Infrastructure.TestDoubles.Mocks;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using StructureMap;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole
{
    public static class Program
    {
        public static void Main()
        {
            var thisAssembly = typeof(Program).Assembly;
            var containerConfig = ContainerConfiguration.CreateFromAssembly(thisAssembly);

            IContainer container = null;

            InitializationService.Initialize(
                containerConfig,
                new LoggerMock(),
                ExecutionContextFactory.CreateDefault().FunctionAppDirectory,
                c => container = c);

            container
                .GetInstance<IConsoleCommandsStartupService>()
                .Start();
        }
    }
}