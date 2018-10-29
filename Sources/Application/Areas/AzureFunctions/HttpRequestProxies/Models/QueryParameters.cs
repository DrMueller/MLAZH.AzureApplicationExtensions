using System.Collections.Generic;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models
{
    public class QueryParameters
    {
        private readonly Dictionary<string, string> _entries;

        public string this[string key] => _entries[key];

        public QueryParameters(Dictionary<string, string> entries)
        {
            _entries = entries;
        }
    }
}