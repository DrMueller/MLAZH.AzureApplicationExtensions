using System.Collections.Generic;
using System.Linq;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Services.TestingServants
{
    internal static class SettingEntryFactory
    {
        internal static SettingEntry Create(object value, params string[] keyParts)
        {
            var allKeyParts = new List<string> { Constants.AzureAppSettingsPrefix }.Concat(keyParts).ToList();

            var keys = allKeyParts.Select(f => new KeyPart(f)).ToList();
            var keyCollection = new KeyPartCollection(keys);

            return new SettingEntry(keyCollection, value.ToString());
        }
    }
}