using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services.Servants
{
    public interface IQueryParametersFactory
    {
        QueryParameters CreateFromCollection(IQueryCollection queryCollection);
    }
}