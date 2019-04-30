using System.Collections.Generic;
using System.Linq;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class KeyPartCollection
    {
        private readonly IReadOnlyCollection<KeyPart> _entries;
        public int Count => _entries.Count;
        public KeyPart FirstEntry => _entries.First();
        public KeyPart LastEntry => _entries.Last();

        public KeyPartCollection(IReadOnlyCollection<KeyPart> entries)
        {
            Guard.ObjectNotNull(() => entries);
            _entries = entries;
        }

        public static KeyPartCollection CreateFromString(string fullKey)
        {
            var keyParts = fullKey.Split('.')
                .Select(f => new KeyPart(f))
                .ToList();

            return new KeyPartCollection(keyParts);
        }

        public KeyPartCollection CreateForNextLevel()
        {
            return new KeyPartCollection(_entries.Skip(1).ToList());
        }
    }
}