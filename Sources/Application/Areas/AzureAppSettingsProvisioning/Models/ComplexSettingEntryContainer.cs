namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class ComplexSettingEntryContainer
    {
        public string Prefix { get; }
        public SettingEntryContainer SettingEntries { get; }

        public ComplexSettingEntryContainer(string prefix, SettingEntryContainer settingEntries)
        {
            Prefix = prefix;
            SettingEntries = settingEntries;
        }
    }
}