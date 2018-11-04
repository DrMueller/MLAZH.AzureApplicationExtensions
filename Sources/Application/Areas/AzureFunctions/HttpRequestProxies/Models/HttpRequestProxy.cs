using Newtonsoft.Json;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models
{
    public class HttpRequestProxy
    {
        private readonly string _jsonBody;
        public QueryParameters QueryParameters { get; }

        public HttpRequestProxy(QueryParameters queryParameters, string jsonBody)
        {
            QueryParameters = queryParameters;
            _jsonBody = jsonBody;
        }

        public T ReadBody<T>()
        {
            return JsonConvert.DeserializeObject<T>(_jsonBody);
        }
    }
}