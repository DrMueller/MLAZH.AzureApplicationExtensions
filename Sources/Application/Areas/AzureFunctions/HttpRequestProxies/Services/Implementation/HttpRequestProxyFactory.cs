using System.IO;
using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services.Servants;
using Newtonsoft.Json;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services.Implementation
{
    internal class HttpRequestProxyFactory : IHttpRequestProxyFactory
    {
        private readonly IQueryParametersFactory _queryParametersFactory;

        public HttpRequestProxyFactory(IQueryParametersFactory queryParametersFactory)
        {
            _queryParametersFactory = queryParametersFactory;
        }

        public HttpRequestProxy CreateFromHttpRequest(HttpRequest httpRequest)
        {
            var queryParameters = _queryParametersFactory.CreateFromCollection(httpRequest.Query);
            var requestBody = new StreamReader(httpRequest.Body).ReadToEnd();
            var body = JsonConvert.DeserializeObject<object>(requestBody);

            return new HttpRequestProxy(queryParameters, body);
        }
    }
}