using System.Linq;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants.Implementation
{
    internal class SettingEntriesFactory : ISettingEntriesFactory
    {
        private readonly IConfigurationRootProxy _configRootProxy;

        public SettingEntriesFactory(IConfigurationRootProxy configRootProxy)
        {
            _configRootProxy = configRootProxy;
        }

        public SettingEntryContainer Create()
        {
            var settingEntries = _configRootProxy
                .GetRootEntries()
                .Select(f => new SettingEntry(KeyPartCollection.CreateFromString(f.Key), f.Value))
                .ToList();

            var result = new SettingEntryContainer(settingEntries);
            return result;
        }
    }
}