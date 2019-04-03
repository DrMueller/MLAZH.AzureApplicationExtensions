using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ErrorHandling.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Servants.Implementation;
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
            For<IHttpRequestProxyFactory>().Use<HttpRequestProxyFactory>().Singleton();
            For<IQueryParametersFactory>().Use<QueryParametersFactory>().Singleton();
        }
    }
}