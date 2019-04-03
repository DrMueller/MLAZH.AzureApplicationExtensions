using System.IO;
using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Servants;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Implementation
{
    public class HttpRequestProxyFactory : IHttpRequestProxyFactory
    {
        private readonly IQueryParametersFactory _queryParametersFactory;

        public HttpRequestProxyFactory(IQueryParametersFactory queryParametersFactory)
        {
            _queryParametersFactory = queryParametersFactory;
        }

        public HttpRequestProxy CreateFromHttpRequest(HttpRequest httpRequest)
        {
            var queryParameters = _queryParametersFactory.CreateFromCollection(httpRequest.Query);
            var bodyJson = new StreamReader(httpRequest.Body).ReadToEnd();
            return new HttpRequestProxy(queryParameters, bodyJson);
        }
    }
}