using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.FunctionContext.Contexts.Services
{
    [TestFixture]
    public class AzureFunctionContextUnitTests
    {
        private Mock<IExceptionHandler> _exceptionHandlerMock;
        private Mock<IHttpRequestProxyFactory> _proxyFactoryMock;
        private Mock<IServiceLocator> _serviceLocatorMock;
        private AzureFunctionContext _sut;

        [SetUp]
        public void Align()
        {
            _exceptionHandlerMock = new Mock<IExceptionHandler>();
            _proxyFactoryMock = new Mock<IHttpRequestProxyFactory>();
            _serviceLocatorMock = new Mock<IServiceLocator>();

            _sut = new AzureFunctionContext(_exceptionHandlerMock.Object, _proxyFactoryMock.Object, _serviceLocatorMock.Object);
        }

        [Test]
        public async Task ExecutingAction_PassesServiceLocator()
        {
            // Arrange
            IServiceLocator actualServiceLocator = null;

            // Act
            await _sut.ExecuteActionAsync(
                locator =>
                {
                    actualServiceLocator = locator;
                    return Task.CompletedTask;
                });

            // Assert
            Assert.AreEqual(_serviceLocatorMock.Object, actualServiceLocator);
        }

        [Test]
        public async Task ExecutingAction_WithException_DoesInvokeActionErrorHandler_WithThrownException()
        {
            // Arrange
            var argumentException = new ArgumentException("Hello Test");

            // Act
            await _sut.ExecuteActionAsync(_ => throw argumentException);

            // Assert
            _exceptionHandlerMock.Verify(f => f.HandleActionExceptionAsync(argumentException), Times.Once);
        }

        [Test]
        public async Task ExecutingAction_WithoutExcepton_DoesNotInvokeAnyErrorHandler()
        {
            // Act
            await _sut.ExecuteActionAsync(locator => Task.CompletedTask);

            // Assert
            _exceptionHandlerMock.Verify(f => f.HandleActionExceptionAsync(It.IsAny<Exception>()), Times.Never);
            _exceptionHandlerMock.Verify(f => f.HandleFunctionExceptionAsync(It.IsAny<Exception>()), Times.Never);
        }

        [Test]
        public async Task ExecutingHttpRequest_InvokesHttpRequestProxyFactory_WithPassedHttpRequest()
        {
            // Arrange
            var httpRequest = new DefaultHttpRequest(new DefaultHttpContext());

            // Act
            await _sut.ExecuteHttpRequestAsync(httpRequest, _ => Task.FromResult<IActionResult>(new OkResult()));

            // Assert
            _proxyFactoryMock.Verify(f => f.CreateFromHttpRequest(httpRequest), Times.Once);
        }

        [Test]
        public async Task ExecutingHttpRequest_PassesHttpRequestContext()
        {
            // Arrange
            var httpRequest = new DefaultHttpRequest(new DefaultHttpContext());
            var htpRequestProxy = new HttpRequestProxy(new QueryParameters(new Dictionary<string, string>()), string.Empty);
            _proxyFactoryMock.Setup(f => f.CreateFromHttpRequest(httpRequest)).Returns(htpRequestProxy);

            HttpRequestContext actualPassedHttpRequestContext = null;

            // Act
            await _sut.ExecuteHttpRequestAsync(
                httpRequest,
                httpRequestContext =>
                {
                    actualPassedHttpRequestContext = httpRequestContext;
                    return Task.FromResult<IActionResult>(new OkResult());
                });

            // Assert
            Assert.IsNotNull(actualPassedHttpRequestContext);
        }

        [Test]
        public async Task ExecutingHttpRequest_WithException_DoesInvokeHttpErrorHandler()
        {
            // Arrange
            var httpRequest = new DefaultHttpRequest(new DefaultHttpContext());
            var argumentException = new ArgumentException("Hello Test");
            var htpRequestProxy = new HttpRequestProxy(new QueryParameters(new Dictionary<string, string>()), string.Empty);
            _proxyFactoryMock.Setup(f => f.CreateFromHttpRequest(httpRequest)).Returns(htpRequestProxy);

            // Act
            await _sut.ExecuteHttpRequestAsync(httpRequest, _ => throw argumentException);

            // Assert
            _exceptionHandlerMock.Verify(f => f.HandleFunctionExceptionAsync(argumentException), Times.Once);
        }

        [Test]
        public async Task ExecutingHttpRequest_WithException_ReturnsActionResult_FromErrorHandler()
        {
            // Arrange
            var httpRequest = new DefaultHttpRequest(new DefaultHttpContext());
            var argumentException = new ArgumentException("Hello Test");
            var htpRequestProxy = new HttpRequestProxy(new QueryParameters(new Dictionary<string, string>()), string.Empty);
            _proxyFactoryMock.Setup(f => f.CreateFromHttpRequest(httpRequest)).Returns(htpRequestProxy);
            var actionResult = new OkResult();
            _exceptionHandlerMock.Setup(
                    f => f
                        .HandleFunctionExceptionAsync(argumentException))
                .Returns(Task.FromResult<IActionResult>(actionResult));

            // Act
            var actualResult = await _sut.ExecuteHttpRequestAsync(httpRequest, _ => throw argumentException);

            // Assert
            Assert.AreEqual(actionResult, actualResult);
        }

        [Test]
        public async Task ExecutingHttpRequest_WithoutException_DoesNotInvokeErrorHandler()
        {
            // Act
            var httpRequest = new DefaultHttpRequest(new DefaultHttpContext());
            var htpRequestProxy = new HttpRequestProxy(new QueryParameters(new Dictionary<string, string>()), string.Empty);
            _proxyFactoryMock.Setup(f => f.CreateFromHttpRequest(httpRequest)).Returns(htpRequestProxy);
            await _sut.ExecuteHttpRequestAsync(httpRequest, _ => Task.FromResult<IActionResult>(new OkResult()));

            // Assert
            _exceptionHandlerMock.Verify(f => f.HandleActionExceptionAsync(It.IsAny<Exception>()), Times.Never);
            _exceptionHandlerMock.Verify(f => f.HandleFunctionExceptionAsync(It.IsAny<Exception>()), Times.Never);
        }
    }
}