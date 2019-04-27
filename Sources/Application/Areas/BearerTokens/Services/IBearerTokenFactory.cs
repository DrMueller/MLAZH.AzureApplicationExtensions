using System.Threading.Tasks;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Models;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Services
{
    public interface IBearerTokenFactory
    {
        Task<BearerToken> CreateAsync(BearerTokenRequest request);
    }
}