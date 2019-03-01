using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Models;
using Mmu.Mlh.LanguageExtensions.Areas.Exceptions;
using Newtonsoft.Json;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services.Implementation
{
    internal class ExceptionHandler : IExceptionHandler
    {
        private readonly ITelemetryClientProxy _telemetryClientProxy;

        public ExceptionHandler(ITelemetryClientProxy telemetryClientProxy)
        {
            _telemetryClientProxy = telemetryClientProxy;
        }

        public Task<IActionResult> HandleExceptionAsync(Exception exception)
        {
            exception = exception.GetMostInnerException();
            var serverError = ServerError.CreateFromException(exception);
            var serializedServerError = JsonConvert.SerializeObject(serverError);

            _telemetryClientProxy.TrackException(exception);

            IActionResult errorActionResult = CreateErrorActionResult(serializedServerError);
            return Task.FromResult(errorActionResult);
        }

        private static ObjectResult CreateErrorActionResult(string serializedServerError)
        {
            var result = new ObjectResult(serializedServerError) { StatusCode = (int)HttpStatusCode.InternalServerError };
            result.ContentTypes.Add("application/json");
            return result;
        }
    }
}