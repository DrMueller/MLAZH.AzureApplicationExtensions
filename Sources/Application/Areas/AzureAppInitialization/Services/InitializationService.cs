using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.BearerTokens.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ExceptionHandling.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Servants;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.HttpRequestProxies.Services.Servants.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.Logging;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.Logging.Implementation;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using StructureMap;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppInitialization.Services
{
    public static class InitializationService
    {
        public static IAzureFunctionContext Initialize(
            ContainerConfiguration containerConfig,
            ILogger logger,
            string localSettingsJsonPath,
            Action<IContainer> afterInitializationCallback = null,
            Action provideDependencenciesCallback = null)
        {
            var container = InitializeContainer(containerConfig, provideDependencenciesCallback);
            InitializeAppSettings(container, localSettingsJsonPath);
            InitializeLogging(container, logger);
            InitializeServices(container);

            afterInitializationCallback?.Invoke(container);
            var context = container.GetInstance<IAzureFunctionContext>();
            return context;
        }

        private static void InitializeAppSettings(IContainer container, string localSettingsJsonPath)
        {
            var configRoot = new ConfigurationBuilder()
                .SetBasePath(localSettingsJsonPath)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            container.Configure(
                cfg =>
                {
                    cfg.For<IConfigurationRoot>().Use(configRoot).Singleton();
                    cfg.For(typeof(IAzureAppSettingsProvider<>)).Use(typeof(AzureAppSettingsProvider<>));
                    cfg.For<IConfigurationRootProxy>().Use<ConfigurationRootProxy>();
                    cfg.For<ISettingEntriesFactory>().Use<SettingEntriesFactory>();
                });
        }

        private static IContainer InitializeContainer(ContainerConfiguration containerConfig, Action provideDependencenciesCallback = null)
        {
            provideDependencenciesCallback?.Invoke();
            var container = ContainerInitializationService.CreateInitializedContainer(containerConfig);

            return container;
        }

        private static void InitializeLogging(IContainer container, ILogger logger)
        {
            container.Configure(
                cfg =>
                {
                    cfg.For<ILogger>().Use(logger).Singleton();
                    cfg.For<ILoggingService>().Use<LoggingService>().Singleton();
                });
        }

        private static void InitializeServices(IContainer container)
        {
            container.Configure(
                cfg =>
                {
                    cfg.For<IExceptionHandler>().Use<ExceptionHandler>().Singleton();
                    cfg.For<IApplicationInsightsInitializationServant>().Use<ApplicationInsightsInitializationServant>().Singleton();
                    cfg.For<ITelemetryClientProxy>().Use<TelemetryClientProxy>().Singleton();
                    cfg.For<IHttpRequestProxyFactory>().Use<HttpRequestProxyFactory>().Singleton();
                    cfg.For<IQueryParametersFactory>().Use<QueryParametersFactory>().Singleton();
                    cfg.For<IBearerTokenFactory>().Use<BearerTokenFactory>().Singleton();
                    cfg.For<IAzureFunctionContext>().Use<AzureFunctionContext>().Transient();
                });
        }
    }
}