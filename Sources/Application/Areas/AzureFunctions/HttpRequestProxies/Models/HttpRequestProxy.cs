using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Models
{
    public class HttpRequestProxy
    {
        private readonly Maybe<object> _body;
        public QueryParameters QueryParameters { get; }

        public HttpRequestProxy(QueryParameters queryParameters, Maybe<object> body)
        {
            QueryParameters = queryParameters;
            _body = body;
        }

        public T ReadBody<T>()
        {
            return _body.Evaluate(actualBody => (T)actualBody, null);
        }
    }
}