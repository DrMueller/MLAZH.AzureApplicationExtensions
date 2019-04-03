using System;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.FunctionContext.Contexts.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using Mmu.Mlh.SettingsProvisioning.Areas.Factories;
using StructureMap;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppInitialization.Services
{
    public static class InitializationService
    {
        public static IAzureFunctionContext Initialize(
            ContainerConfiguration containerConfig,
            Action<IContainer> afterInitializationCallback = null,
            Action provideDependencenciesCallback = null)
        {
            var container = InitializeContainer(containerConfig, provideDependencenciesCallback);
            afterInitializationCallback?.Invoke(container);
            var context = container.GetInstance<IAzureFunctionContext>();
            return context;
        }

        public static IAzureFunctionContext Initialize<TSettings>(
            ContainerConfiguration containerConfig,
            string settingsSectionKey,
            string environmentName,
            Action<IContainer> afterInitializationCallback = null,
            Action provideDependencenciesCallback = null)
            where TSettings : class, new()
        {
            var container = InitializeContainer(containerConfig, provideDependencenciesCallback);
            var settings = container
                .GetInstance<ISettingsFactory>()
                .CreateSettings<TSettings>(settingsSectionKey, environmentName, containerConfig.RootAssembly.CodeBase);

            container.Configure(cfg => cfg.For<TSettings>().Use(settings).Singleton());

            afterInitializationCallback?.Invoke(container);
            var context = container.GetInstance<IAzureFunctionContext>();
            return context;
        }

        private static IContainer InitializeContainer(ContainerConfiguration containerConfig, Action provideDependencenciesCallback = null)
        {
            provideDependencenciesCallback?.Invoke();
            var container = ContainerInitializationService.CreateInitializedContainer(containerConfig);
            return container;
        }
    }
}