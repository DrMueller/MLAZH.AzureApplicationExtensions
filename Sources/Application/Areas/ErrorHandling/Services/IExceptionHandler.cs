using System;
using Microsoft.AspNetCore.Mvc;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services
{
    public interface IExceptionHandler
    {
        IActionResult HandleException(Exception exception);
    }
}