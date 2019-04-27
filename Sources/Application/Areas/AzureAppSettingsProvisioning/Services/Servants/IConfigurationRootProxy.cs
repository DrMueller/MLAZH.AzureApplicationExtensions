using System.Collections.Generic;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants
{
    internal interface IConfigurationRootProxy
    {
        IEnumerable<KeyValuePair<string, string>> GetRootEntries();
    }
}