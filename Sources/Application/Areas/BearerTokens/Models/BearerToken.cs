using System;
using Mmu.Mlh.LanguageExtensions.Areas.DateTimes;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Models
{
    public class BearerToken
    {
        public string AccessToken { get; }
        public string Resource { get; }
        public UtcDateTime ExpiresOn { get; }

        public BearerToken(UtcDateTime expiresOn, string resource, string accessToken)
        {
            Guard.StringNotNullOrEmpty(() => resource);
            Guard.StringNotNullOrEmpty(() => accessToken);

            ExpiresOn = expiresOn;
            Resource = resource;
            AccessToken = accessToken;
        }
    }
}