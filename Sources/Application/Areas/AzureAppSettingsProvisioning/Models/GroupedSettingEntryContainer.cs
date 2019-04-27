namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class GroupedSettingEntryContainer
    {
        public string Prefix { get; }
        public SettingEntryContainer SettingEntries { get; }

        public GroupedSettingEntryContainer(string prefix, SettingEntryContainer settingEntries)
        {
            Prefix = prefix;
            SettingEntries = settingEntries;
        }
    }
}