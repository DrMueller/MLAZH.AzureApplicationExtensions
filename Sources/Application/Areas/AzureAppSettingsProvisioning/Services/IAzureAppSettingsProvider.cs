namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services
{
    public interface IAzureAppSettingsProvider<TSettings>
        where TSettings : new()
    {
        TSettings ProvideSettings();
    }
}