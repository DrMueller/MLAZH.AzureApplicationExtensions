using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Models
{
    public class BearerTokenRequest
    {
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string GrantType { get; }
        public string PrincipalId { get; }
        public string TargetHost { get; }
        public string TenantId { get; }

        public BearerTokenRequest(string tenantId, string principalId, string targetHost, string clientId, string clientSecret, string grantType)
        {
            Guard.StringNotNullOrEmpty(() => tenantId);
            Guard.StringNotNullOrEmpty(() => principalId);
            Guard.StringNotNullOrEmpty(() => targetHost);
            Guard.StringNotNullOrEmpty(() => clientId);
            Guard.StringNotNullOrEmpty(() => clientSecret);
            Guard.StringNotNullOrEmpty(() => grantType);

            TenantId = tenantId;
            PrincipalId = principalId;
            TargetHost = targetHost;
            ClientId = clientId;
            ClientSecret = clientSecret;
            GrantType = grantType;
        }
    }
}