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
        }
    }
}