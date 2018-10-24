using System;
using Microsoft.ApplicationInsights;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Implementation
{
    internal class TelemetryClientProxy : ITelemetryClientProxy
    {
        private readonly TelemetryClient _telemtryClient;

        public TelemetryClientProxy(IApplicationInsightsInitializationServant initServant)
        {
            initServant.AssureApplictionInsightsIsInitialized();
            _telemtryClient = new TelemetryClient();
        }

        public void TrackEvent(string eventName)
        {
            _telemtryClient.TrackEvent(eventName);
        }

        public void TrackException(Exception exception)
        {
            _telemtryClient.TrackException(exception);
        }
    }
}