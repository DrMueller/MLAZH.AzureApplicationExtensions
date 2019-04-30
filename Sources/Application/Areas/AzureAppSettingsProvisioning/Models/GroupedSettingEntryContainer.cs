using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class GroupedSettingEntryContainer
    {
        public string Prefix { get; }
        public SettingEntryContainer SettingEntries { get; }

        public GroupedSettingEntryContainer(string prefix, SettingEntryContainer settingEntries)
        {
            Guard.StringNotNullOrEmpty(() => prefix);
            Guard.ObjectNotNull(() => settingEntries);

            Prefix = prefix;
            SettingEntries = settingEntries;
        }
    }
}