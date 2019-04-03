using System.Collections.Generic;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models
{
    public class QueryParameters
    {
        private readonly Dictionary<string, string> _entries;

        public string this[string key] => _entries[key];

        public QueryParameters(Dictionary<string, string> entries)
        {
            _entries = entries;
        }

        public bool ContainsKey(string key)
        {
            return _entries.ContainsKey(key);
        }
    }
}