using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services
{
    public interface IExceptionHandler
    {
        Task HandleActionExceptionAsync(Exception exception);

        Task<IActionResult> HandleFunctionExceptionAsync(Exception exception);
    }
}