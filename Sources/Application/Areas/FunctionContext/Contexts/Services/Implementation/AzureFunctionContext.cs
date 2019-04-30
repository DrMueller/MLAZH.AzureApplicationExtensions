using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services.Implementation
{
    internal class AzureFunctionContext : IAzureFunctionContext
    {
        private readonly IExceptionHandler _exceptionHandler;
        private readonly IHttpRequestProxyFactory _proxyFactory;
        private readonly IServiceLocator _serviceLocator;

        public AzureFunctionContext(
            IExceptionHandler exceptionHandler,
            IHttpRequestProxyFactory proxyFactory,
            IServiceLocator serviceLocator)
        {
            _exceptionHandler = exceptionHandler;
            _proxyFactory = proxyFactory;
            _serviceLocator = serviceLocator;
        }

        public async Task ExecuteActionAsync(Func<IServiceLocator, Task> callback)
        {
            try
            {
                await callback(_serviceLocator);
            }
            catch (Exception ex)
            {
                await _exceptionHandler.HandleActionExceptionAsync(ex);
            }
        }

        public async Task<IActionResult> ExecuteHttpRequestAsync(HttpRequest httpRequest, Func<HttpRequestContext, Task<IActionResult>> callback)
        {
            try
            {
                var requestProxy = _proxyFactory.CreateFromHttpRequest(httpRequest);
                var context = new HttpRequestContext(_serviceLocator, requestProxy);
                return await callback(context);
            }
            catch (Exception ex)
            {
                return await _exceptionHandler.HandleFunctionExceptionAsync(ex);
            }
        }
    }
}