using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
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
                        // See: https://stackoverflow.com/questions/51308053/application-insights-working-without-config
                        // Setup Channel, Initializers, and Sampling
                        // Nugets Required: "Microsoft.ApplicationInsights", "Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel"
                        var channel = new ServerTelemetryChannel();
                        var config = TelemetryConfiguration.Active;
                        config.InstrumentationKey = _appInsightsSettings.InstrumentationKey;
                        channel.Initialize(config);
                        TelemetryConfiguration.Active.TelemetryChannel = channel;
                        config.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
                        config.TelemetryProcessorChainBuilder.UseAdaptiveSampling();
                        _isInitialized = true;
                    }
                }
            }
        }
    }
}