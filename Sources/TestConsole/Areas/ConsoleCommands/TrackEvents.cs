using System;
using System.Threading.Tasks;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlh.ConsoleExtensions.Areas.Commands.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Areas.ConsoleCommands
{
    public class TrackEvents : IConsoleCommand
    {
        public string Description { get; } = "Track Events";
        public ConsoleKey Key { get; } = ConsoleKey.D2;

        public Task ExecuteAsync()
        {
            var telemetryClientProxy = ServiceLocatorSingleton.Instance.GetService<ITelemetryClientProxy>();
            telemetryClientProxy.TrackEvent("Hell Test 1");
            telemetryClientProxy.TrackEvent("Hell Test 2");
            telemetryClientProxy.TrackEvent("Hell Test 3");

            return Task.CompletedTask;
        }
    }
}