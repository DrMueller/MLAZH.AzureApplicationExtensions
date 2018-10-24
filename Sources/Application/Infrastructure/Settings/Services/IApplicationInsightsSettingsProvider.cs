using Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.Settings.Services
{
    public interface IApplicationInsightsSettingsProvider
    {
        ApplicationInsightsSettings ProvideApplicationInsightsSettings();
    }
}