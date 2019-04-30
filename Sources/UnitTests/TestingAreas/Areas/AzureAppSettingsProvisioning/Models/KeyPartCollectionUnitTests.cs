using System.Collections.Generic;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Models
{
    [TestFixture]
    public class KeyPartCollectionUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            var firstKeyPart = new KeyPart("1");
            var middleKeyPart = new KeyPart("2");
            var lastKeyPart = new KeyPart("2");
            var entries = new List<KeyPart>
            {
                firstKeyPart,
                middleKeyPart,
                lastKeyPart
            };

            ConstructorTestBuilderFactory
                .Constructing<KeyPartCollection>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null).Fails()
                .WithArgumentValues(entries)
                .Maps()
                .ToProperty(f => f.Count).WithValue(entries.Count)
                .ToProperty(f => f.FirstEntry).WithValue(firstKeyPart)
                .ToProperty(f => f.LastEntry).WithValue(lastKeyPart)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void CreatingFromString_CreatesCollectionFromString()
        {
            // Arrange
            const string Keys = "Key1.SubKey1.SubKey2";

            // Act
            var actualCollection = KeyPartCollection.CreateFromString(Keys);

            // Assert
            Assert.AreEqual(3, actualCollection.Count);
            Assert.AreEqual("Key1", actualCollection.FirstEntry.Value);
            Assert.AreEqual("SubKey2", actualCollection.LastEntry.Value);
        }

        [Test]
        public void CreatingNextLevel_CratesNextLevel()
        {
            // Arrange
            const string Keys = "Key1.SubKey1.SubKey2";
            var sut = KeyPartCollection.CreateFromString(Keys);

            // Act
            var actualCollection = sut.CreateForNextLevel();

            Assert.AreEqual(2, actualCollection.Count);
            Assert.AreEqual("SubKey1", actualCollection.FirstEntry.Value);
            Assert.AreEqual("SubKey2", actualCollection.LastEntry.Value);
        }
    }
}