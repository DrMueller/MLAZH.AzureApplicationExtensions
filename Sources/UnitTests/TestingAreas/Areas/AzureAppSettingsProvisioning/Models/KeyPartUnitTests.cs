using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Models
{
    [TestFixture]
    public class KeyPartUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            const string KeyPartValue = "Value";

            ConstructorTestBuilderFactory
                .Constructing<KeyPart>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null).Fails()
                .WithArgumentValues(string.Empty).Fails()
                .WithArgumentValues(KeyPartValue)
                .Maps()
                .ToProperty(f => f.Value).WithValue(KeyPartValue)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void GettingTrailingNumbers_GetsTrailingNumbers()
        {
            // Arrange
            const int TrailingNumbers = 123;

            var sut = new KeyPart("Value" + TrailingNumbers);

            // Act
            var actualNumbers = sut.GetTrailingNumbers();

            // Assert
            Assert.AreEqual(TrailingNumbers.ToString(), actualNumbers);
        }

        [Test]
        public void GettingValueWithoutTrailingNumbers_GetsValueWithTrailingNumber()
        {
            // Arrang
            const string ValueWithoutNumbers = "Tra";
            const int TrailingNumbers = 123;

            var sut = new KeyPart(ValueWithoutNumbers + TrailingNumbers);

            // Act
            var actualValue = sut.GetValueWithoutTrailingNumbers();

            // Assert
            Assert.AreEqual(ValueWithoutNumbers, actualValue);
        }
    }
}