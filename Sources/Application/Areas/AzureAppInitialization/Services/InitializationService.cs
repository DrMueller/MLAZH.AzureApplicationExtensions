using System;
using System.Reflection;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
using Mmu.Mlh.SettingsProvisioning.Areas.Factories;
using StructureMap;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppInitialization.Services
{
    public static class InitializationService
    {
        private static Container _container;
        private static bool _servicesAreInitialized;
        private static object _servicesLock = new object();
        private static bool _settingsAreInitialized;
        private static object _settingsLock = new object();

        public static void AssureServicesAreInitialized(ContainerConfiguration containerConfig, Action provideDependencenciesCallback = null)
        {
            if (_servicesAreInitialized)
            {
                return;
            }

            lock (_servicesLock)
            {
                if (_servicesAreInitialized)
                {
                    return;
                }

                provideDependencenciesCallback?.Invoke();
                _container = ContainerInitializationService.CreateInitializedContainer(containerConfig);
                _servicesAreInitialized = true;
            }
        }

        public static void AssureSettingsAreInitialized<TSettings>(
            string settingsSectionKey,
            string environmentName,
            Assembly rootAssembly)
            where TSettings : class, new()
        {
            if (_settingsAreInitialized)
            {
                return;
            }

            lock (_settingsLock)
            {
                if (_settingsAreInitialized)
                {
                    return;
                }

                var settings = _container
                    .GetInstance<ISettingsFactory>()
                    .CreateSettings<TSettings>(settingsSectionKey, environmentName, rootAssembly.CodeBase);

                _container.Configure(cfg => cfg.For<TSettings>().Use(settings).Singleton());
                _settingsAreInitialized = true;
            }
        }
    }
}