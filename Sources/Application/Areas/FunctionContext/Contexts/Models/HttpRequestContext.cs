using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Models;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;
using Mmu.Mlh.ServiceProvisioning.Areas.Provisioning.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Models
{
    public class HttpRequestContext
    {
        public HttpRequestProxy RequestProxy { get; }
        public IServiceLocator ServiceLocator { get; }

        public HttpRequestContext(IServiceLocator serviceLocator, HttpRequestProxy requestProxy)
        {
            Guard.ObjectNotNull(() => serviceLocator);
            Guard.ObjectNotNull(() => requestProxy);

            ServiceLocator = serviceLocator;
            RequestProxy = requestProxy;
        }
    }
}