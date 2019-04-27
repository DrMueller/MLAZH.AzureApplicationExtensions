using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants;
using Mmu.Mlh.LanguageExtensions.Areas.Collections;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Implementation
{
    internal class AzureAppSettingsProvider<TSettings> : IAzureAppSettingsProvider<TSettings>
        where TSettings : new()
    {
        private readonly ISettingEntriesFactory _settingEntriesFactory;

        public AzureAppSettingsProvider(ISettingEntriesFactory settingEntriesFactory)
        {
            _settingEntriesFactory = settingEntriesFactory;
        }

        public TSettings ProvideSettings()
        {
            var settings = new TSettings();
            var settingEntries = _settingEntriesFactory.Create();
            var mySettingEntries = settingEntries.GetMyProperties(Constants.AzureAppSettingsPrefix);

            ProcessProperty(mySettingEntries, settings);

            return settings;
        }

        private void ApplyPropertyValue(
            SettingEntry settingEntry,
            object objectToSet)
        {
            var objectProps = objectToSet.GetType().GetProperties().ToList();
            var propInfo = objectProps.Single(f => f.Name == settingEntry.PropertyName);

            if (!string.IsNullOrEmpty(settingEntry.Value))
            {
                var propInfoType = propInfo.PropertyType;
                object entryValue = settingEntry.Value;

                if (propInfoType != typeof(string))
                {
                    var typeConverter = TypeDescriptor.GetConverter(propInfoType);
                    entryValue = typeConverter.ConvertFromInvariantString(settingEntry.Value);
                }

                propInfo.SetValue(objectToSet, entryValue);
            }
        }

        private void ProcessProperty(SettingEntryContainer settingEntries, object objectToProcess)
        {
            settingEntries
                .GetSimplePropertyEntries()
                .ForEach(f => ApplyPropertyValue(f, objectToProcess));

            var complexEntries = settingEntries.GetComplexSettingEntries();

            if (complexEntries.Any())
            {
                var objectProps = objectToProcess.GetType().GetProperties().Where(f => f.CanWrite).ToList();
                foreach (var complexEntry in complexEntries)
                {
                    var propInfo = objectProps.Single(f => f.Name == complexEntry.Prefix);
                    var propertyInstance = Activator.CreateInstance(propInfo.PropertyType);
                    propInfo.SetValue(objectToProcess, propertyInstance);

                    var mySettingEntries = complexEntry.SettingEntries.GetMyProperties(complexEntry.Prefix);
                    ProcessProperty(mySettingEntries, propertyInstance);
                }
            }
        }
    }
}