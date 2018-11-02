using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctions.HttpRequestProxies.Services.Servants.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FileStorage;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FileStorage.Implementation;
using StructureMap;

namespace Mmu.Mlazh.AzureApplicationExtensions.Infrastructure.DependencyInjection
{
    public class AzureApplicationExtensionsRegistry : Registry
    {
        public AzureApplicationExtensionsRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<AzureApplicationExtensionsRegistry>();
                    scanner.WithDefaultConventions();
                });

            For<IExceptionHandler>().Use<ExceptionHandler>().Singleton();
            For<IApplicationInsightsInitializationServant>().Use<ApplicationInsightsInitializationServant>().Singleton();
            For<ITelemetryClientProxy>().Use<TelemetryClientProxy>().Singleton();
            For<IFileService>().Use<FileService>().Singleton();
            For<IHttpRequestProxyFactory>().Use<HttpRequestProxyFactory>().Singleton();
            For<IQueryParametersFactory>().Use<QueryParametersFactory>().Singleton();
        }
    }
}