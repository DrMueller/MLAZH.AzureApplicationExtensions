using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Servants
{
    public interface IQueryParametersFactory
    {
        QueryParameters CreateFromCollection(IQueryCollection queryCollection);
    }
}