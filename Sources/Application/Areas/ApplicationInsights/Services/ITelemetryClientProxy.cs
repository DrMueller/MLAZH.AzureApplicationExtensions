using System;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services
{
    public interface ITelemetryClientProxy
    {
        void TrackEvent(string eventName);

        void TrackException(Exception exception);
    }
}