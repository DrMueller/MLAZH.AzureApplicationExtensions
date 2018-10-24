using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Services;
using StructureMap;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole
{
    public class TestConsoleRegistry : Registry
    {
        public TestConsoleRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<TestConsoleRegistry>();
                    scanner.WithDefaultConventions();
                });

            For<IApplicationInsightsSettingsProvider>().Use<ApplicationInsightsSettingsProvider>().Singleton();
        }
    }
}