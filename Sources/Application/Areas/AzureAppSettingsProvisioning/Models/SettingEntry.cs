using System;
using System.ComponentModel;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models
{
    public class SettingEntry
    {
        private readonly KeyPartCollection _keyParts;
        public KeyPart FirstKeyPart => _keyParts.FirstEntry;
        public bool IsCollection => _keyParts.FirstEntry.HasTrailingNumber;
        public bool IsComplex => _keyParts.Count > 1;
        public string PropertyName => _keyParts.LastEntry.Value;
        public string Value { get; }

        public SettingEntry(KeyPartCollection keyParts, string value)
        {
            Guard.ObjectNotNull(() => keyParts);

            _keyParts = keyParts;
            Value = value;
        }

        public object GetConvertedValue(Type targetType)
        {
            if (targetType == typeof(string))
            {
                return Value;
            }

            var typeConverter = TypeDescriptor.GetConverter(targetType);
            return typeConverter.ConvertFromInvariantString(Value);
        }

        public SettingEntry CreateNextKeyPartLevelEntry()
        {
            return new SettingEntry(
                _keyParts.CreateForNextLevel(),
                Value);
        }
    }
}