using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;
using Mmu.Mlh.TestingExtensions.Areas.Common.BasesClasses;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.IntegrationTests.TestingAreas.Areas.AzureFunctions.Context
{
    [TestFixture]
    public class AzureFunctionExecutionContextIntegrationTests : TestingBaseWithContainer
    {
        private AzureFunctionContext _sut;
        private Mock<IExceptionHandler> _exceptionHandlerMock;
        private Mock<IHttpRequestProxyFactory> _proxyFactoryMock;
        private Mock<IServiceLocator> _serviceLocatorMock;

        [SetUp]
        public void Align()
        {
            _exceptionHandlerMock = new Mock<IExceptionHandler>();
            _proxyFactoryMock = new Mock<IHttpRequestProxyFactory>();
            _serviceLocatorMock = new Mock<IServiceLocator>();
            _sut = new AzureFunctionContext(_exceptionHandlerMock.Object, _proxyFactoryMock.Object, _serviceLocatorMock.Object);
        }

        [Test]
        public async Task ExecutingAction_ReturnsServiceLocator()
        {
            // Act
            IServiceLocator actualServiceLocator = null;

            await _sut.ExecuteActionAsync(serviceLocator =>
            {
                actualServiceLocator = serviceLocator;
                return null;
            });

            // Assert
            Assert.AreEqual(_serviceLocatorMock.Object, actualServiceLocator);
        }

        [Test]
        public async Task ExecutingAction_ThrowingException_CallsExceptionHandler_WithException()
        {
            // Arrange
            var exception = new Exception("Test");
            Exception actualException = null;
            _exceptionHandlerMock.Setup(f => f.HandleActionExceptionAsync(It.IsAny<Exception>())).Callback<Exception>(
                    passedException =>
                    {
                        actualException = passedException;
                    })
                .Returns(() => Task.FromResult<IActionResult>(new OkResult()));

            RegisterInstance(_exceptionHandlerMock.Object);

            // Act
            await _sut.ExecuteActionAsync(service => throw exception);

            // Assert
            Assert.AreEqual(exception, actualException);
        }
    }
}