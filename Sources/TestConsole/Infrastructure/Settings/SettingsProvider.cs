using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services;
using Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Settings;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Infrastructure.Settings
{
    public class SettingsProvider : IApplicationInsightsSettingsProvider
    {
        private readonly AppSettings _appSettings;

        public SettingsProvider(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public ApplicationInsightsSettings ProvideApplicationInsightsSettings()
        {
            return _appSettings.ApplicationInsightsSettings;
        }
    }
}