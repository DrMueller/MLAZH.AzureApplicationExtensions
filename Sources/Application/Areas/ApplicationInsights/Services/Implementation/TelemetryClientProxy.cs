using System;
using Microsoft.ApplicationInsights;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Implementation
{
    internal class TelemetryClientProxy : ITelemetryClientProxy
    {
        public TelemetryClientProxy(IApplicationInsightsInitializationServant initServant)
        {
            &initServant.AssureApplictionInsightsIsInitialized();
        }

        public void TrackEvent(string eventName)
        {
            var telemetryClient = new TelemetryClient();
            telemetryClient.TrackEvent(eventName);
            telemetryClient.Flush();
        }

        public void TrackException(Exception exception)
        {
            var telemetryClient = new TelemetryClient();
            telemetryClient.TrackException(exception);
            telemetryClient.Flush();
        }
    }
}