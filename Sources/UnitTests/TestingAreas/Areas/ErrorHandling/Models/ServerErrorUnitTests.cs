using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.ErrorHandling.Models
{
    [TestFixture]
    public class ServerErrorUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            const string Message = "TestMessage";
            const string TypeName = "TestTypeName";
            const string StackTrace = "TestStackTrace";

            ConstructorTestBuilderFactory
                .Constructing<ServerError>()
                .UsingDefaultConstructor()
                .WithArgumentValues(null, TypeName, StackTrace).Fails()
                .WithArgumentValues(Message, null, StackTrace).Fails()
                .WithArgumentValues(Message, TypeName, null)
                .Maps()
                .ToProperty(f => f.StackTrace).WithValue(string.Empty)
                .BuildMaps()
                .WithArgumentValues(Message, TypeName, StackTrace)
                .Maps()
                .ToProperty(f => f.Message).WithValue(Message)
                .ToProperty(f => f.StackTrace).WithValue(StackTrace)
                .ToProperty(f => f.TypeName).WithValue(TypeName)
                .BuildMaps()
                .Assert();
        }

        [Test]
        public void CreatingFromException_MapsToProperties()
        {
            // Arrange
            const string Message = "TestMessage";
            var exception = new IgnoreException(Message);

            // Act
            var actualServerError = ServerError.CreateFromException(exception);

            // Assert
            Assert.AreEqual(Message, actualServerError.Message);
            Assert.AreEqual(exception.GetType().Name, actualServerError.TypeName);
            Assert.IsEmpty(actualServerError.StackTrace);
        }
    }
}