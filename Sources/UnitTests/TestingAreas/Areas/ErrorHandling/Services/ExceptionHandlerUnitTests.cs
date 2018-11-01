using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FileStorage;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.ErrorHandling.Services
{
    [TestFixture]
    public class ExceptionHandlerUnitTests
    {
        private Mock<IFileService> _fileServiceMock;
        private ExceptionHandler _sut;
        private Mock<ITelemetryClientProxy> _telemetryClientProxyMock;

        [SetUp]
        public void Align()
        {
            _fileServiceMock = new Mock<IFileService>();
            _telemetryClientProxyMock = new Mock<ITelemetryClientProxy>();
            _sut = new ExceptionHandler(_telemetryClientProxyMock.Object, _fileServiceMock.Object);
        }

        [Test]
        public async Task HandlingException_CallsFileService_Once()
        {
            // Arrange
            var exception = new Exception("Test");

            // Act
            await _sut.HandleExceptionAsync(exception);

            // Assert
            _fileServiceMock.Verify(f => f.AppendAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task HandlingException_CallsFileService_WithServerError()
        {
            // Arrange
            const string ExceptionMessage = "Test";
            var exception = new Exception(ExceptionMessage);
            var serverError = ServerError.CreateFromException(exception);
            var expectedSerializedServerError = JsonConvert.SerializeObject(serverError);

            // Act
            await _sut.HandleExceptionAsync(exception);

            // Assert
            _fileServiceMock.Verify(f => f.AppendAsync(expectedSerializedServerError));
        }

        [Test]
        public async Task HandlingException_CallsTrackException_Once()
        {
            // Arrange
            var exception = new Exception("Test");

            // Act
            await _sut.HandleExceptionAsync(exception);

            // Assert
            _telemetryClientProxyMock.Verify(f => f.TrackException(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        public async Task HandlingException_CallsTrackException_WithPassedException()
        {
            // Arrange
            var exception = new Exception("Test");

            // Act
            await _sut.HandleExceptionAsync(exception);

            // Assert
            _telemetryClientProxyMock.Verify(f => f.TrackException(exception));
        }

        [Test]
        public async Task HandlingException_Returns_ContentTypeApplicationJson()
        {
            // Arrange
            var exception = new Exception("Test");

            // Act
            var actualObjectResult = (ObjectResult)await _sut.HandleExceptionAsync(exception);

            // Assert
            Assert.IsTrue(actualObjectResult.ContentTypes.Contains("application/json"));
        }

        [Test]
        public async Task HandlingException_Returns_ObjectResult()
        {
            // Arrange
            var exception = new Exception("Test");
            var serverError = ServerError.CreateFromException(exception);

            // Act
            var actualActionResult = await _sut.HandleExceptionAsync(exception);

            // Assert
            Assert.That(actualActionResult, Is.TypeOf<ObjectResult>());
        }

        [Test]
        public async Task HandlingException_Returns_ObjectResult_With_ServerErrorAsValue()
        {
            // Arrange
            const string ExceptionMessage = "Test";
            var exception = new Exception(ExceptionMessage);
            var serverError = ServerError.CreateFromException(exception);
            var expectedSerializedServerError = JsonConvert.SerializeObject(serverError);

            // Act
            var actualObjectResult = (ObjectResult)await _sut.HandleExceptionAsync(exception);

            // Assert
            Assert.AreEqual(expectedSerializedServerError, actualObjectResult.Value);
        }

        [Test]
        public async Task HandlingException_Returns_Status500()
        {
            // Arrange
            var exception = new Exception("Test");

            // Act
            var actualObjectResult = (ObjectResult)await _sut.HandleExceptionAsync(exception);

            // Assert
            Assert.AreEqual(500, actualObjectResult.StatusCode);
        }
    }
}