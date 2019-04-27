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

        public IReadOnlyCollection<GroupedSettingEntryContainer> GetComplexEntries()
        {
            var result = GetSingleComplexEntries()
                .Concat(GetArrayComplexEntries())
                .ToList();

            return result;
        }

        public SettingEntryContainer GetPropertiesByFirstKeyPartWithoutNumber(string firstKeyPart)
        {
            var entries = Entries.Where(f => f.FirstKeyPart.GetValueWithoutTrailingNumbers() == firstKeyPart)
                .Select(f => f.CreateNextKeyPartLevelEntry())
                .ToList();

            return new SettingEntryContainer(entries);
        }

        public IReadOnlyCollection<GroupedSettingEntryContainer> GetSimpleArrayPropertyEntries()
        {
            var colEntries = Entries.Where(f => !f.IsComplex && f.IsCollection)
                .ToList();

            var grpedEntries = colEntries
                .GroupBy(f => f.FirstKeyPart.GetValueWithoutTrailingNumbers())
                .Select(f => new GroupedSettingEntryContainer(f.Key, new SettingEntryContainer(f.ToList())))
                .ToList();

            return grpedEntries;
        }

        public IReadOnlyCollection<SettingEntry> GetSimpleSinglePropertyEntries()
        {
            return Entries.Where(f => !f.IsComplex && !f.IsCollection).ToList();
        }

        private IReadOnlyCollection<GroupedSettingEntryContainer> GetArrayComplexEntries()
        {
            var result = new List<GroupedSettingEntryContainer>();
            var colEntries = Entries.Where(f => f.IsComplex && f.IsCollection).ToList();
            var grpdByType = colEntries.GroupBy(f => f.FirstKeyPart.GetValueWithoutTrailingNumbers());

            foreach (var typeGrp in grpdByType)
            {
                var grpdByNumber = typeGrp.GroupBy(f => f.FirstKeyPart.GetTrailingNumbers());

                foreach (var numGrp in grpdByNumber)
                {
                    result.Add(new GroupedSettingEntryContainer(typeGrp.Key, new SettingEntryContainer(numGrp.ToList())));
                }
            }

            return result;
        }

        private IReadOnlyCollection<GroupedSettingEntryContainer> GetSingleComplexEntries()
        {
            var complexEntries = Entries.Where(f => f.IsComplex && !f.IsCollection);
            var grpdByPrefix = complexEntries.GroupBy(f => f.FirstKeyPart.Value);

            var entries = grpdByPrefix.Select(
                    f =>
                        new GroupedSettingEntryContainer(f.Key, new SettingEntryContainer(f.ToList())))
                .ToList();

            return entries;
        }
    }
}