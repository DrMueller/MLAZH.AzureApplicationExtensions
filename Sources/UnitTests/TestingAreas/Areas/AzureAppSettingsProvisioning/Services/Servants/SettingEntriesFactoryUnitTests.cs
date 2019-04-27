using System.Collections.Generic;
using System.Linq;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants.Implementation;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Services.Servants
{
    [TestFixture]
    public class SettingEntriesFactoryUnitTests
    {
        private Mock<IConfigurationRootProxy> _configRootProxyMock;
        private SettingEntriesFactory _sut;

        [SetUp]
        public void Align()
        {
            _configRootProxyMock = new Mock<IConfigurationRootProxy>();
            _sut = new SettingEntriesFactory(_configRootProxyMock.Object);
        }

        [Test]
        public void CreatingEntries_HavingNoConfigs_ReturnsCollectionWithoutEntries()
        {
            // Arrange
            _configRootProxyMock.Setup(f => f.GetRootEntries()).Returns(new List<KeyValuePair<string, string>>());

            // Act
            var actualEntries = _sut.Create();

            // Assert
            CollectionAssert.IsEmpty(actualEntries.Entries);
        }

        [Test]
        public void CreatingEntries_WithComplexKeys_ReturnsComplexEntries()
        {
            // Arrange
            var mockRootEntries = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Key1.SubKey1.SubSubKey2", "Value1"),
                new KeyValuePair<string, string>("Key2.SubKey1", "Value2"),
                new KeyValuePair<string, string>("Key3.SubKey3", "Value3")
            };

            _configRootProxyMock.Setup(f => f.GetRootEntries()).Returns(mockRootEntries);

            // Act
            var actualEntries = _sut.Create();

            // Assert
            Assert.IsNotNull(actualEntries.Entries);
            Assert.AreEqual(actualEntries.Entries.Count, mockRootEntries.Count);
            Assert.AreEqual("SubSubKey2", actualEntries.Entries.ElementAt(0).PropertyName);
            Assert.AreEqual("SubKey1", actualEntries.Entries.ElementAt(1).PropertyName);
            Assert.AreEqual("SubKey3", actualEntries.Entries.ElementAt(2).PropertyName);
        }

        [Test]
        public void CreatingEntries_WithPrimitiveKeys_ReturnsPrimitiveEntries()
        {
            // Arrange
            var mockRootEntries = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Key1", "Value1"),
                new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")
            };

            _configRootProxyMock.Setup(f => f.GetRootEntries()).Returns(mockRootEntries);

            // Act
            var actualEntries = _sut.Create();

            // Assert
            Assert.IsNotNull(actualEntries.Entries);
            Assert.AreEqual(actualEntries.Entries.Count, mockRootEntries.Count);
            Assert.AreEqual(mockRootEntries[0].Value, actualEntries.Entries.ElementAt(0).Value);
            Assert.AreEqual(mockRootEntries[0].Key, actualEntries.Entries.ElementAt(0).FirstKeyPart.Value);
            Assert.AreEqual(mockRootEntries[1].Value, actualEntries.Entries.ElementAt(1).Value);
            Assert.AreEqual(mockRootEntries[1].Key, actualEntries.Entries.ElementAt(1).FirstKeyPart.Value);
            Assert.AreEqual(mockRootEntries[2].Value, actualEntries.Entries.ElementAt(2).Value);
            Assert.AreEqual(mockRootEntries[2].Key, actualEntries.Entries.ElementAt(2).FirstKeyPart.Value);
        }
    }
}