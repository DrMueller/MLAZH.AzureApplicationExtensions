using System.Collections.Generic;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Models
{
    [TestFixture]
    public class SettingEntryUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            const string FirstKeyValue = "FirstKey";

            var keyPartCollection = new KeyPartCollection(
                new List<KeyPart>
                {
                    new KeyPart(FirstKeyValue),
                    new KeyPart("Second"),
                    new KeyPart("Third")
                });

            const string EntryValue = "Hello Entry";

            ConstructorTestBuilderFactory
                .Constructing<SettingEntry>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null, string.Empty)
                .Fails()
                .WithArgumentValues(keyPartCollection, EntryValue)
                .Maps()
                .ToProperty(f => f.Value).WithValue(EntryValue)
                .ToProperty(f => f.FirstKeyPart.Value).WithValue(FirstKeyValue)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void CreatingNextLevel_ReturnsNextKeyLevel_WithSameValue()
        {
            // Arrange
            const string EntryValue = "Hello Entry";
            const string Level2Key = "Level2";

            var keyParts = new KeyPartCollection(
                new List<KeyPart>
                {
                    new KeyPart("Level1"),
                    new KeyPart(Level2Key)
                });

            var sut = new SettingEntry(keyParts, EntryValue);

            // Act
            var actualEntry = sut.CreateNextKeyPartLevelEntry();

            // Assert
            Assert.AreEqual(actualEntry.FirstKeyPart.Value, Level2Key);
            Assert.AreEqual(actualEntry.Value, EntryValue);
        }

        [Test]
        public void GettingConvertedValue_TargetTypeBeingLong_ReturnsLong()
        {
            // Arrange
            var longType = typeof(long);
            const long Number = 123;
            var keyParts = new KeyPartCollection(new List<KeyPart>());

            var sut = new SettingEntry(keyParts, Number.ToString());

            // Act
            var actualValue = sut.GetConvertedValue(longType);

            // Assert
            Assert.AreEqual(longType, actualValue.GetType());
            Assert.AreEqual(Number, actualValue);
        }

        [Test]
        public void GettingConvertedValue_TargetTypeBeingString_ReturnsString()
        {
            // Arrange
            var stringType = typeof(string);
            const string String = "Hello";
            var keyParts = new KeyPartCollection(new List<KeyPart>());

            var sut = new SettingEntry(keyParts, String);

            // Act
            var actualValue = sut.GetConvertedValue(stringType);

            // Assert
            Assert.AreEqual(stringType, actualValue.GetType());
            Assert.AreEqual(String, actualValue);
        }
    }
}