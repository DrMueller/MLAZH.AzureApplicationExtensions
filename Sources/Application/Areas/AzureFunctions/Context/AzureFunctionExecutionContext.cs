using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.Context
{
    public static class AzureFunctionExecutionContext
    {
        public static async Task<IActionResult> ExecuteAsync<TService>(
            Func<TService, Task<IActionResult>> callback)
        {
            try
            {
                var requestedService = ServiceLocatorSingleton.Instance.GetService<TService>();
                return await callback(requestedService);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var exceptionHandler = ServiceLocatorSingleton.Instance.GetService<IExceptionHandler>();
                return await exceptionHandler.HandleExceptionAsync(ex);
            }
        }

        public static async Task<IActionResult> ExecuteAsync<TService>(
            HttpRequest httpRequest,
            Func<TService, HttpRequestProxy, Task<IActionResult>> callback)
        {
            try
            {
                var requestedService = ServiceLocatorSingleton.Instance.GetService<TService>();
                var httpRequestProxy = ServiceLocatorSingleton
                    .Instance
                    .GetService<IHttpRequestProxyFactory>()
                    .CreateFromHttpRequest(httpRequest);

                return await callback(requestedService, httpRequestProxy);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var exceptionHandler = ServiceLocatorSingleton.Instance.GetService<IExceptionHandler>();
                return await exceptionHandler.HandleExceptionAsync(ex);
            }
        }
    }
}