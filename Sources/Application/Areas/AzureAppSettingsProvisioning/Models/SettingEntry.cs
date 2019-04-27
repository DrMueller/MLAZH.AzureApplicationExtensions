using System;
using System.Linq;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class SettingEntry
    {
        public bool IsComplex => Key.Contains(".");
        public string Key { get; }

        public string Prefix
        {
            get
            {
                if (Key.Contains("."))
                {
                    return Key.Substring(0, Key.IndexOf(".", StringComparison.OrdinalIgnoreCase));
                }

                return string.Empty;
            }
        }

        public string PropertyName => Key.Split('.').Last();
        public string Value { get; }

        public SettingEntry(string key, string value)
        {
            Guard.StringNotNullOrEmpty(() => key);

            Key = key;
            Value = value;
        }
    }
}