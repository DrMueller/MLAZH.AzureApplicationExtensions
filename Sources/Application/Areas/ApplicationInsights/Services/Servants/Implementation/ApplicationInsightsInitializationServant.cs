using Microsoft.ApplicationInsights.Extensibility;
using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants.Implementation
{
    internal class ApplicationInsightsInitializationServant : IApplicationInsightsInitializationServant
    {
        private ApplicationInsightsSettings _appInsightsSettings;
        private bool _isInitialized;
        private object _lock = new object();

        public ApplicationInsightsInitializationServant(IApplicationInsightsSettingsProvider appInsightsSettingsprovider)
        {
            _appInsightsSettings = appInsightsSettingsprovider.ProvideApplicationInsightsSettings();
        }

        public void AssureApplictionInsightsIsInitialized()
        {
            if (!_isInitialized)
            {
                lock (_lock)
                {
                    if (!_isInitialized)
                    {
                        TelemetryConfiguration.Active.InstrumentationKey = _appInsightsSettings.InstrumentationKey;
                        _isInitialized = true;
                    }
                }
            }
        }
    }
}