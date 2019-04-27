using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants
{
    internal interface ISettingEntriesFactory
    {
        SettingEntryContainer Create();
    }
}