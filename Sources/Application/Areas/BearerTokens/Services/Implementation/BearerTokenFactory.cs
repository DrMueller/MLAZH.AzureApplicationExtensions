using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Models;
using Mmu.Mlh.LanguageExtensions.Areas.DateTimes;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.RestCallBuilding;
using Mmu.Mlh.RestExtensions.Areas.RestProxies;
using Newtonsoft.Json.Linq;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Services.Implementation
{
    internal class BearerTokenFactory : IBearerTokenFactory
    {
        private readonly IRestCallBuilderFactory _restCallBuilderFactory;
        private readonly IRestProxy _restProxy;

        public BearerTokenFactory(IRestProxy restProxy, IRestCallBuilderFactory restCallBuilderFactory)
        {
            _restProxy = restProxy;
            _restCallBuilderFactory = restCallBuilderFactory;
        }

        public async Task<BearerToken> CreateAsync(BearerTokenRequest request)
        {
            var restCall = CreateRestCall(request);
            var resultContent = await _restProxy.PerformCallAsync<string>(restCall);
            return ParseBearerToken(resultContent);
        }

        private static RestCallBody CreateBody(BearerTokenRequest request)
        {
            var clientId = $"{request.ClientId}@{request.TenantId}";
            var resource = $"{request.PrincipalId}/{request.TargetHost}@{request.TenantId}";

            var dict = new Dictionary<string, string>
            {
                { "grant_type", request.GrantType },
                { "client_id", clientId },
                { "client_secret", request.ClientSecret },
                { "resource", resource }
            };

            var result = RestCallBody.CreateApplicationWwwFormUrlEncoded(dict);
            return result;
        }

        private static BearerToken ParseBearerToken(string resultContent)
        {
            var dynamicObject = JObject.Parse(resultContent);
            var expiresIn = dynamicObject.Value<long>("expires_in");
            var expiresInHours = expiresIn / 60 / 60;
            var exipresInDate = DateTime.UtcNow.AddHours(expiresInHours);
            var utcExpiresIn = UtcDateTime.CreateFromDateTime(exipresInDate);

            return new BearerToken(
                utcExpiresIn,
                dynamicObject.Value<string>("resource"),
                dynamicObject.Value<string>("access_token"));
        }

        private RestCall CreateRestCall(BearerTokenRequest request)
        {
            var requestUrl = new Uri($"https://accounts.accesscontrol.windows.net/{request.TenantId}/tokens/OAuth/2");

            var restCall = _restCallBuilderFactory
                .StartBuilding(requestUrl, RestCallMethodType.Post)
                .WithBody(CreateBody(request))
                .Build();

            return restCall;
        }
    }
}