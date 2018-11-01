using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.IntegrationTests.TestingInfrastructure.Settings
{
    public class TestApplicationInsightsSettingsProvider : IApplicationInsightsSettingsProvider
    {
        public ApplicationInsightsSettings ProvideApplicationInsightsSettings()
        {
            return new ApplicationInsightsSettings
            {
                InstrumentationKey = "1234"
            };
        }
    }
}