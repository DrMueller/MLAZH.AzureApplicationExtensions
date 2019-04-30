using System.Collections.Generic;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Models
{
    [TestFixture]
    public class GroupedSettingEntryContainerUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            const string Prefix = "Pref";
            var container = new SettingEntryContainer(new List<SettingEntry>());

            ConstructorTestBuilderFactory
                .Constructing<GroupedSettingEntryContainer>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null, container).Fails()
                .WithArgumentValues(string.Empty, container).Fails()
                .WithArgumentValues(Prefix, null).Fails()
                .WithArgumentValues(Prefix, container)
                .Maps()
                .ToProperty(f => f.Prefix).WithValue(Prefix)
                .ToProperty(f => f.SettingEntries).WithValue(container)
                .BuildMaps()
                .Assert();
        }
    }
}