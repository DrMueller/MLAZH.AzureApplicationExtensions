using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Infrastructure.Settings
{
    public class SettingsProvider : IApplicationInsightsSettingsProvider
    {
        public ApplicationInsightsSettings ProvideApplicationInsightsSettings()
        {
            return new ApplicationInsightsSettings
            {
                InstrumentationKey = "0a170130 - 1a5e - 4880 - 879b - b8b7e22deb4"
            };
        }
    }
}