using System;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Newtonsoft.Json;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models
{
    public class HttpRequestProxy
    {
        public string JsonBody { get; }
        public QueryParameters QueryParameters { get; }

        public HttpRequestProxy(QueryParameters queryParameters, string jsonBody)
        {
            Guard.ObjectNotNull(() => queryParameters);

            QueryParameters = queryParameters;
            JsonBody = jsonBody;
        }

        public bool TryReadingBody<T>(out T body)
        {
            try
            {
                body = JsonConvert.DeserializeObject<T>(JsonBody);
                return body != null;
            }
            catch (Exception)
            {
                body = default(T);
                return false;
            }
        }
    }
}