using System;
using System.Net;
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

        public IActionResult HandleException(Exception exception)
        {
            exception = exception.GetMostInnerException();
            _telemetryClientProxy.TrackException(exception);
            return CreateErrorActionResult(exception);
        }

        private static ObjectResult CreateErrorActionResult(Exception exception)
        {
            var serverError = ServerError.CreateFromException(exception);
            var serializedServerError = JsonConvert.SerializeObject(serverError);

            var result = new ObjectResult(serializedServerError) { StatusCode = (int)HttpStatusCode.InternalServerError };
            result.ContentTypes.Add("application/json");

            return result;
        }
    }
}