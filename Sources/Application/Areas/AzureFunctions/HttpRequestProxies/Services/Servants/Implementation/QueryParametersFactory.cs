using System.Linq;
using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services.Servants.Implementation
{
    internal class QueryParametersFactory : IQueryParametersFactory
    {
        public QueryParameters CreateFromCollection(IQueryCollection queryCollection)
        {
            var entries = queryCollection.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.ToString());
            return new QueryParameters(entries);
        }
    }
}