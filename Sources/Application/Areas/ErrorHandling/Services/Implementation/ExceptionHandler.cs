using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FileStorage;
using Mmu.Mlh.LanguageExtensions.Areas.Exceptions;
using Newtonsoft.Json;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services.Implementation
{
    internal class ExceptionHandler : IExceptionHandler
    {
        private readonly IFileService _fileService;
        private readonly ITelemetryClientProxy _telemetryClientProxy;

        public ExceptionHandler(ITelemetryClientProxy telemetryClientProxy, IFileService fileService)
        {
            _telemetryClientProxy = telemetryClientProxy;
            _fileService = fileService;
        }

        public async Task<IActionResult> HandleExceptionAsync(Exception exception)
        {
            exception = exception.GetMostInnerException();
            var serverError = ServerError.CreateFromException(exception);
            var serializedServerError = JsonConvert.SerializeObject(serverError);

            await _fileService.AppendAsync(serializedServerError);
            _telemetryClientProxy.TrackException(exception);

            return CreateErrorActionResult(serializedServerError);
        }

        private static ObjectResult CreateErrorActionResult(string serializedServerError)
        {
            var result = new ObjectResult(serializedServerError) { StatusCode = (int)HttpStatusCode.InternalServerError };
            result.ContentTypes.Add("application/json");
            return result;
        }
    }
}