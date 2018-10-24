using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Services
{
    public class ApplicationInsightsSettingsProvider : IApplicationInsightsSettingsProvider
    {
        public ApplicationInsightsSettings ProvideApplicationInsightsSettings()
        {
            return new ApplicationInsightsSettings
            {
                InstrumentationKey = "46e45a38-9948-4aa8-ae72-f090b9914477"
            };
        }
    }
}