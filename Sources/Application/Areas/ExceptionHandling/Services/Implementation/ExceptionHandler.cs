using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Models;
using Newtonsoft.Json;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services.Implementation
{
    internal class ExceptionHandler : IExceptionHandler
    {
        private readonly ITelemetryClientProxy _telemetryClientProxy;

        public ExceptionHandler(ITelemetryClientProxy telemetryClientProxy)
        {
            _telemetryClientProxy = telemetryClientProxy;
        }

        public Task HandleActionExceptionAsync(Exception exception)
        {
            UnpackAndLogException(exception);
            return Task.CompletedTask;
        }

        public Task<IActionResult> HandleFunctionExceptionAsync(Exception exception)
        {
            var innerException = UnpackAndLogException(exception);

            var serverError = ServerError.CreateFromException(innerException);
            var serializedServerError = JsonConvert.SerializeObject(serverError);

            IActionResult errorActionResult = CreateErrorActionResult(serializedServerError);
            return Task.FromResult(errorActionResult);
        }

        private static ObjectResult CreateErrorActionResult(string serializedServerError)
        {
            var result = new ObjectResult(serializedServerError) { StatusCode = (int)HttpStatusCode.InternalServerError };
            result.ContentTypes.Add("application/json");
            return result;
        }

        private Exception UnpackAndLogException(Exception exception)
        {
            var innerException = exception;
            while (innerException.InnerException != null)
            {
                innerException = innerException.InnerException;
            }

            _telemetryClientProxy.TrackException(exception);
            _telemetryClientProxy.TrackException(innerException);
            return innerException;
        }
    }
}