using System.Collections.Generic;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Models
{
    [TestFixture]
    public class SettingEntryContainerUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            var entries = new List<SettingEntry>
            {
                new SettingEntry(new KeyPartCollection(new List<KeyPart>()), "Value")
            };

            ConstructorTestBuilderFactory
                .Constructing<SettingEntryContainer>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null)
                .Fails()
                .WithArgumentValues(entries)
                .Maps().ToProperty(f => f.Entries).WithValue(entries)
                .BuildMaps()
                .Assert();
        }
        

        [Test]
        public void GettingComplexEntries_GetsEntries()
        {
            // Arrange

            // Act
            
            // Assert

        }

        [Test]
        public void GetEntries_ByFirstKeyPartWithoutNumber_GetsEntries()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void GettingSimpleArrayEntries_GetsEntries()
        {
            // Arrange

            // Act

            // Assert

        }

        [Test]
        public void GettingSimplePrimitiveEntries_GetsEntries()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}