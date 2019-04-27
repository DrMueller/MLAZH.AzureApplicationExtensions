using System;
using System.Collections.Generic;
using System.Linq;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class SettingEntryContainer
    {
        public IReadOnlyCollection<SettingEntry> Entries { get; }

        public SettingEntryContainer(IReadOnlyCollection<SettingEntry> entries)
        {
            Guard.ObjectNotNull(() => entries);

            Entries = entries;
        }

        public IReadOnlyCollection<ComplexSettingEntryContainer> GetComplexSettingEntries()
        {
            var complexEntries = Entries.Where(f => f.IsComplex);
            var grpd = complexEntries.GroupBy(f => f.Prefix);

            var entries = grpd.Select(f => new ComplexSettingEntryContainer(f.Key, new SettingEntryContainer(f.ToList()))).ToList();
            return entries;
        }

        public SettingEntryContainer GetMyProperties(string keyPrefix)
        {
            var entries = Entries.Where(f => f.Key.StartsWith(keyPrefix + ".", StringComparison.OrdinalIgnoreCase))
                .Select(f => new SettingEntry(f.Key.Replace(keyPrefix + ".", string.Empty), f.Value))
                .ToList();

            return new SettingEntryContainer(entries);
        }

        public IReadOnlyCollection<SettingEntry> GetSimplePropertyEntries()
        {
            return Entries.Where(f => !f.IsComplex).ToList();
        }
    }
}