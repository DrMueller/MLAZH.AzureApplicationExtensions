using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctionExecution.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services;
using Mmu.Mlh.ApplicationExtensions.Areas.ServiceProvisioning;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctionExecution.Services
{
    public static class AzureFunctionExecutionContext
    {
        public static async Task<IActionResult> ExecuteAsync<TService>(
            Func<TService, Task<IActionResult>> callback,
            Assembly rootAssembly,
            Action provideDependencenciesCallback = null)
        {
            ServicesInitializationServant.AssureServicesAreInitialized(rootAssembly, provideDependencenciesCallback);

            try
            {
                var requestedService = ProvisioningServiceSingleton.Instance.GetService<TService>();
                return await callback(requestedService);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var exceptionHandler = ProvisioningServiceSingleton.Instance.GetService<IExceptionHandler>();
                return exceptionHandler.HandleException(ex);
            }
        }
    }
}