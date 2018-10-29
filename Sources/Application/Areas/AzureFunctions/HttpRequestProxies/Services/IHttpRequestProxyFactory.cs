using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services
{
    public interface IHttpRequestProxyFactory
    {
        HttpRequestProxy CreateFromHttpRequest(HttpRequest httpRequest);
    }
}