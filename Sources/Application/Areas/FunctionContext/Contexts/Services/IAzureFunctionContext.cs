using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services
{
    public interface IAzureFunctionContext
    {
        Task ExecuteActionAsync(Func<IServiceLocator, Task> callback);

        Task<IActionResult> ExecuteHttpRequestAsync(HttpRequest httpRequest, Func<HttpRequestContext, Task<IActionResult>> callback);
    }
}