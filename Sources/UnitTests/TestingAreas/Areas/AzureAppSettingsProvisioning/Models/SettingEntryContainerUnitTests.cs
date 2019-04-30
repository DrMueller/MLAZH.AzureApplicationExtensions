namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
    using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
    using NUnit.Framework;

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
        public void GettingComplexEntries_GetsComplexEntries()
        {
            // Arrange
            var nonComplexEntry = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("tra") }), "NonComplex");
            var complexEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("tra"), new KeyPart("Tra2") }), "Value1");
            var complexEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("tra3"), new KeyPart("Tra4") }), "Value2");

            var entries = new List<SettingEntry>()
            {
                nonComplexEntry,
                complexEntry1,
                complexEntry2
            };

            var sut = new SettingEntryContainer(entries);

            // Act
            var actualEntries = sut.GetComplexEntries();

            // Assert
            var flatEntries = actualEntries.SelectMany(f => f.SettingEntries.Entries);
            Assert.IsTrue(flatEntries.All(f => f.IsComplex));
        }

        [Test]
        public void GetEntries_ByFirstKeyPartWithoutNumber_GetsEntries()
        {
            // Arrange
            var entry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("nottra") }), "NonComplex");
            var otherEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("Tra6"), new KeyPart("Tra2") }), "Value1");
            var otherEntry3 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("Tra3"), new KeyPart("Tra4") }), "Value2");

            var entries = new List<SettingEntry>()
            {
                entry1,
                otherEntry2,
                otherEntry3
            };

            var sut = new SettingEntryContainer(entries);

            // Act
            var actual = sut.GetEntriesByFirstKeyPartWithoutNumber("Tra");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Entries.Count);
            Assert.IsTrue(actual.Entries.All(f => f.FirstKeyPart.Value.StartsWith("Tra", StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void GettingPrimitiveArrayEntries_GetsEntries()
        {
            // Arrange
            var primitiveArrayEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("primitiveArrayEntry1") }), "NonComplex");
            var primitiveArrayEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("primitiveArrayEntry2") }), "NonComplex");
            var primitiveSingleEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("primitive") }), "NonComplex");
            var primitiveSingleEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("anotherPrimitive") }), "NonComplex");

            var complexArrayEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("complexArrayEntry1"), new KeyPart("Arr1") }), "NonComplex");
            var complexArrayEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("complexArrayEntry2"), new KeyPart("Arr2") }), "NonComplex");
            var complexSingleEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("complex"), new KeyPart("Tra") }), "NonComplex");
            var complexSingleEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("anotherComplex"), new KeyPart("Tra2") }), "NonComplex");

            var entries = new List<SettingEntry>()
            {
                primitiveArrayEntry1,
                primitiveArrayEntry2,
                primitiveSingleEntry1,
                primitiveSingleEntry2,
                complexArrayEntry1,
                complexArrayEntry2,
                complexSingleEntry1,
                complexSingleEntry2
            };

            var sut = new SettingEntryContainer(entries);

            // Act
            var actualEntries = sut.GetPrimitiveArrayEntries();

            // Assert
            var flatEntries = actualEntries.SelectMany(f => f.SettingEntries.Entries);

            Assert.IsNotNull(actualEntries);
            Assert.AreEqual(1, actualEntries.Count);
            Assert.IsTrue(flatEntries.All(f => !f.IsComplex && f.IsCollection));
        }

        [Test]
        public void GettingPrimitiveSingleEntries_GetsEntries()
        {
            // Arrange
            var primitiveArrayEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("primitiveArrayEntry1") }), "NonComplex");
            var primitiveArrayEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("primitiveArrayEntry2") }), "NonComplex");
            var primitiveSingleEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("primitive") }), "NonComplex");
            var primitiveSingleEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("anotherPrimitive") }), "NonComplex");

            var complexArrayEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("complexArrayEntry1"), new KeyPart("Arr1") }), "NonComplex");
            var complexArrayEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("complexArrayEntry2"), new KeyPart("Arr2") }), "NonComplex");
            var complexSingleEntry1 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("complex"), new KeyPart("Tra") }), "NonComplex");
            var complexSingleEntry2 = new SettingEntry(new KeyPartCollection(new List<KeyPart> { new KeyPart("anotherComplex"), new KeyPart("Tra2") }), "NonComplex");

            var entries = new List<SettingEntry>()
            {
                primitiveArrayEntry1,
                primitiveArrayEntry2,
                primitiveSingleEntry1,
                primitiveSingleEntry2,
                complexArrayEntry1,
                complexArrayEntry2,
                complexSingleEntry1,
                complexSingleEntry2
            };

            var sut = new SettingEntryContainer(entries);

            // Act
            var actualEntries = sut.GetPrimitiveSingleEntries();

            // Assert
            Assert.IsNotNull(actualEntries);
            Assert.AreEqual(2, actualEntries.Count);
            Assert.IsTrue(actualEntries.All(f => !f.IsComplex && !f.IsCollection));
        }
    }
}