using System;
using System.Collections;
using System.Collections.Generic;
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
            var mySettingEntries = settingEntries.GetEntriesByFirstKeyPartWithoutNumber(Constants.AzureAppSettingsPrefix);

            ProcessSettingEntries(mySettingEntries, settings);
            return settings;
        }

        private static void ApplyPropertyValue(
            SettingEntry settingEntry,
            object objectToSet)
        {
            var objectProps = objectToSet.GetType().GetProperties().ToList();
            var propInfo = objectProps.Single(f => f.Name == settingEntry.PropertyName);
            propInfo.SetValue(objectToSet, settingEntry.GetConvertedValue(propInfo.PropertyType));
        }

        private static IList AssurePropertyListIsInitialized(string keyPrefix, object objectToProcess)
        {
            var propInfo = objectToProcess.GetType().GetProperties().Single(f => f.Name == keyPrefix);

            var existingValue = propInfo.GetValue(objectToProcess);
            if (existingValue != null)
            {
                return (IList)existingValue;
            }

            var genericType = propInfo.PropertyType.GetGenericArguments()[0];
            var genericListType = typeof(List<>).MakeGenericType(genericType);
            var list = (IList)Activator.CreateInstance(genericListType);
            propInfo.SetValue(objectToProcess, list);

            return list;
        }

        private static void ProcessSimpleArrayEntries(GroupedSettingEntryContainer entries, object objectToProcess)
        {
            var list = AssurePropertyListIsInitialized(entries.Prefix, objectToProcess);
            var genericType = list.GetType().GetGenericArguments()[0];
            entries.SettingEntries.Entries.ForEach(f => list.Add(f.GetConvertedValue(genericType)));
        }

        private void ProcessComplexEntry(
            GroupedSettingEntryContainer complexEntry,
            object objectToProcess)
        {
            var objectProps = objectToProcess.GetType().GetProperties().Where(f => f.CanWrite).ToList();
            var propInfo = objectProps.Single(f => f.Name == complexEntry.Prefix);
            object propertyInstance;

            if (typeof(IEnumerable).IsAssignableFrom(propInfo.PropertyType))
            {
                var genericType = propInfo.PropertyType.GetGenericArguments()[0];
                propertyInstance = Activator.CreateInstance(genericType);
                var list = AssurePropertyListIsInitialized(complexEntry.Prefix, objectToProcess);
                list.Add(propertyInstance);
            }
            else
            {
                propertyInstance = Activator.CreateInstance(propInfo.PropertyType);
                propInfo.SetValue(objectToProcess, propertyInstance);
            }

            var mySettingEntries = complexEntry.SettingEntries.GetEntriesByFirstKeyPartWithoutNumber(complexEntry.Prefix);
            ProcessSettingEntries(mySettingEntries, propertyInstance);
        }

        private void ProcessSettingEntries(SettingEntryContainer settingEntries, object objectToProcess)
        {
            settingEntries
                .GetPrimitiveSingleEntries()
                .ForEach(f => ApplyPropertyValue(f, objectToProcess));

            settingEntries.GetPrimitiveArrayEntries()
                .ForEach(f => ProcessSimpleArrayEntries(f, objectToProcess));

            settingEntries.GetComplexEntries()
                .ForEach(f => ProcessComplexEntry(f, objectToProcess));
        }
    }
}