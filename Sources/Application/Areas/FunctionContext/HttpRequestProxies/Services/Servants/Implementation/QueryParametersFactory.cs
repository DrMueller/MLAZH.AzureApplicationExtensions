using System.Linq;
using Microsoft.AspNetCore.Http;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Servants.Implementation
{
    public class QueryParametersFactory : IQueryParametersFactory
    {
        public QueryParameters CreateFromCollection(IQueryCollection queryCollection)
        {
            var entries = queryCollection.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.ToString());
            return new QueryParameters(entries);
        }
    }
}