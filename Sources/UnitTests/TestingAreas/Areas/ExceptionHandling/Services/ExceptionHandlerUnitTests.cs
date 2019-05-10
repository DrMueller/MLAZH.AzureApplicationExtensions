using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.Logging;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.ExceptionHandling.Services
{
    [TestFixture]
    public class ExceptionHandlerUnitTests
    {
        private ExceptionHandler _sut;
        private Mock<ILoggingService> _loggingServiceMock;

        [SetUp]
        public void Align()
        {
            _loggingServiceMock = new Mock<ILoggingService>();
            _sut = new ExceptionHandler(_loggingServiceMock.Object);
        }

        [Test]
        public async Task HandlingActionException_UnpacksAndLogsExceptions()
        {
            // Arrange
            var innerException = new ArgumentException("Hello Test");
            var exception = new Exception("Test Exception", innerException);

            // Act
            await _sut.HandleActionExceptionAsync(exception);

            // Assert
            _loggingServiceMock.Verify(f => f.LogError(innerException), Times.Once);
            _loggingServiceMock.Verify(f => f.LogError(exception), Times.Once);
        }

        [Test]
        public async Task HandlingFunctionException_ReturnsServerErrorObject()
        {
            // Arrange
            const string InnerExceptionMessage = "Hello Test";
            var innerException = new ArgumentException(InnerExceptionMessage);
            var exception = new Exception("Test Exception", innerException);

            // Act
            var actionResult = await _sut.HandleFunctionExceptionAsync(exception);

            // Assert
            var actualObjectResult = actionResult as ObjectResult;
            Assert.IsNotNull(actualObjectResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actualObjectResult.StatusCode);

            Assert.IsTrue(actualObjectResult.Value.ToString().Contains(InnerExceptionMessage, StringComparison.Ordinal));
        }

        [Test]
        public async Task HandlingFunctionException_UnpacksAndLogsExceptions()
        {
            // Arrange
            var innerException = new ArgumentException("Hello Test");
            var exception = new Exception("Test Exception", innerException);

            // Act
            await _sut.HandleFunctionExceptionAsync(exception);

            // Assert
            _loggingServiceMock.Verify(f => f.LogError(innerException), Times.Once);
            _loggingServiceMock.Verify(f => f.LogError(exception), Times.Once);
        }
    }
}