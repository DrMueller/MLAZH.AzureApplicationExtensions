using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services;
using Mmu.Mlazh.AzureApplicationExtensions.IntegrationTests.TestingInfrastructure.Settings;
using StructureMap;

namespace Mmu.Mlazh.AzureApplicationExtensions.IntegrationTests.TestingInfrastructure.DependencyInjection
{
    public class IntegrationTestsRegistry : Registry
    {
        public IntegrationTestsRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<IntegrationTestsRegistry>();
                    scanner.WithDefaultConventions();
                });

            For<IApplicationInsightsSettingsProvider>().Use<TestApplicationInsightsSettingsProvider>();
        }
    }
}