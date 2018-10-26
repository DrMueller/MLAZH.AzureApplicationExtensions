using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctionExecution
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
                return exceptionHandler.HandleException(ex);
            }
        }
    }
}