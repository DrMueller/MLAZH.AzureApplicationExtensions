using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.Context;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services;
using Mmu.Mlazh.AzureApplicationExtensions.IntegrationTests.TestingInfrastructure.TestServices;
using Mmu.Mlh.TestingExtensions.Areas.Common.BasesClasses;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.IntegrationTests.TestingAreas.Areas.AzureFunctions.Context
{
    [TestFixture]
    public class AzureFunctionExecutionContextIntegrationTests : TestingBaseWithContainer
    {
        [Test]
        public async Task Executing_ReturnsRequestedService()
        {
            // Arrange
            var testServiceMock = new Mock<ITestService>();
            RegisterInstance(testServiceMock.Object);

            // Act
            ITestService actualService = null;
            await AzureFunctionExecutionContext.ExecuteAsync<ITestService>(
                service =>
                {
                    actualService = service;
                    return null;
                });

            // Assert
            Assert.AreEqual(testServiceMock.Object, actualService);
        }

        [Test]
        public async Task Executing_ThrowingException_CallsExceptionHandler_WithException()
        {
            // Arrange
            var exception = new Exception("Test");
            var testServiceMock = new Mock<IExceptionHandler>();
            Exception actualException = null;
            testServiceMock.Setup(f => f.HandleExceptionAsync(It.IsAny<Exception>())).Callback<Exception>(
                    passedException =>
                    {
                        actualException = passedException;
                    })
                .Returns(() => Task.FromResult<IActionResult>(new OkResult()));

            RegisterInstance(testServiceMock.Object);

            // Act
            await AzureFunctionExecutionContext.ExecuteAsync<ITestService>(
                service => throw exception);

            // Assert
            Assert.AreEqual(exception, actualException);
        }
    }
}