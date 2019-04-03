using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services
{
    public interface IHttpRequestProxyFactory
    {
        HttpRequestProxy CreateFromHttpRequest(HttpRequest httpRequest);
    }
}