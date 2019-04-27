using System.Collections.Generic;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Services.TestingModels;
using Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Services.TestingServants;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Services
{
    [TestFixture]
    public class AzureAppSettingsProviderUnitTests
    {
        private Mock<ISettingEntriesFactory> _settingEntriesFactory;
        private AzureAppSettingsProvider<TestSettings> _sut;

        [SetUp]
        public void Align()
        {
            _settingEntriesFactory = new Mock<ISettingEntriesFactory>();
            _sut = new AzureAppSettingsProvider<TestSettings>(_settingEntriesFactory.Object);
        }

        [Test]
        public void Loading_Collections_ComplexObjects_LoadsComplexObjectCollection()
        {
            // Arrange
            var expectedObjects = new List<ComplexObject>
            {
                new ComplexObject
                {
                    StringValue = "Hello 1"
                },
                new ComplexObject
                {
                    StringValue = "Hello 2"
                }
            };

            // Act
            var actualSettings = _sut.ProvideSettings();

            // Assert
            CollectionAssert.AreEqual(expectedObjects, actualSettings.ComplexObjects);
        }

        [Test]
        public void Loading_Collections_Strings_LoadsStringsCollection()
        {
            // Arrange
            var expectedStrings = new List<string>
            {
                "String1",
                "String2",
                "String3"
            };

            // Act
            var actualSettings = _sut.ProvideSettings();

            // Assert
            CollectionAssert.AreEqual(expectedStrings, actualSettings.Strings);
        }

        [Test]
        public void Loading_IntegerProperty_PropertyIsSet_LoadsAsInteger()
        {
            // Arrange
            const int ExpectedInt = 3;

            var settingEntry = SettingEntryFactory.Create(ExpectedInt, nameof(TestSettings.IntValue));

            var settingEntries = new SettingEntryContainer(
                new List<SettingEntry>
                {
                    settingEntry
                });

            _settingEntriesFactory.Setup(f => f.Create()).Returns(settingEntries);

            // Act
            var actualSettings = _sut.ProvideSettings();

            // Assert
            Assert.AreEqual(ExpectedInt, actualSettings.IntValue);
        }

        [Test]
        public void Loading_Single_ComplexObjectProperty_PropertyIsSet_LoadsObject()
        {
            // Arrange
            const string ExpectedStringValue = "Hello1";
            const string ExpectedAnotherStringValue = "Hello2";
            const string ExpectedSecondObjectStringValue = "Hello3";

            var settings1 = SettingEntryFactory.Create(ExpectedStringValue, nameof(TestSettings.MyComplexObject), nameof(ComplexObject.StringValue));
            var settings2 = SettingEntryFactory.Create(ExpectedAnotherStringValue, nameof(TestSettings.MyComplexObject), nameof(ComplexObject.AnotherStringValue));
            var settings3 = SettingEntryFactory.Create(ExpectedSecondObjectStringValue, nameof(TestSettings.AnotherComplexObject), nameof(ComplexObject.StringValue));

            var settingEntries = new SettingEntryContainer(
                new List<SettingEntry>
                {
                    settings1,
                    settings2,
                    settings3
                });

            _settingEntriesFactory.Setup(f => f.Create()).Returns(settingEntries);

            // Act
            var actualSettings = _sut.ProvideSettings();

            // Assert
            Assert.IsNotNull(actualSettings.MyComplexObject);
            Assert.AreEqual(ExpectedStringValue, actualSettings.MyComplexObject.StringValue);
            Assert.AreEqual(ExpectedAnotherStringValue, actualSettings.MyComplexObject.AnotherStringValue);

            Assert.IsNotNull(actualSettings.AnotherComplexObject);
            Assert.AreEqual(ExpectedSecondObjectStringValue, actualSettings.AnotherComplexObject.StringValue);
        }

        [Test]
        public void Loading_Single_ComplexObjectProperty_WithSubObject_LoadsSubObject()
        {
            // Arrange
            const string ExpectedStringValue = "Hello1";
            var settings1 = SettingEntryFactory.Create(ExpectedStringValue, nameof(TestSettings.MyComplexObject), nameof(ComplexObject.SubObject), nameof(SubObject.StringSubObjectProp));
            var settingEntries = new SettingEntryContainer(
                new List<SettingEntry>
                {
                    settings1
                });

            _settingEntriesFactory.Setup(f => f.Create()).Returns(settingEntries);

            // Act
            var actualSettings = _sut.ProvideSettings();
            var actualSubObject = actualSettings.MyComplexObject.SubObject;

            // Assert
            Assert.IsNotNull(actualSubObject);
            Assert.AreEqual(ExpectedStringValue, actualSubObject.StringSubObjectProp);
        }

        [Test]
        public void Loading_StringProperty_PropertyIsSet_LoadsAsString()
        {
            // Arrange
            const string ExpectedString = "Hello 1";

            var settingEntry = SettingEntryFactory.Create(ExpectedString, nameof(TestSettings.StringValue));

            var settingEntries = new SettingEntryContainer(
                new List<SettingEntry>
                {
                    settingEntry
                });

            _settingEntriesFactory.Setup(f => f.Create()).Returns(settingEntries);

            // Act
            var actualSettings = _sut.ProvideSettings();

            // Assert
            Assert.AreEqual(ExpectedString, actualSettings.StringValue);
        }
    }
}