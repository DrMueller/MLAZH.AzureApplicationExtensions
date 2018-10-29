﻿using System;
using System.Reflection;
using Mmu.Mlh.NetStandardExtensions.Areas.SettingsProvisioning.Models;
using Mmu.Mlh.NetStandardExtensions.Areas.SettingsProvisioning.Services;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Services;
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
            if (!_servicesAreInitialized)
            {
                lock (_servicesLock)
                {
                    if (!_servicesAreInitialized)
                    {
                        provideDependencenciesCallback?.Invoke();
                        _container = ContainerInitializationService.CreateInitializedContainer(containerConfig);
                        _servicesAreInitialized = true;
                    }
                }
            }
        }

        public static void AssureSettingsAreInitialized<TSettings>(string settingsSectionKey, Assembly rootAssembly)
            where TSettings : class, new()
        {
            if (!_settingsAreInitialized)
            {
                lock (_settingsLock)
                {
                    if (!_settingsAreInitialized)
                    {
                        var settings = SettingsFactory.CreateSettings<TSettings>(settingsSectionKey, SettingsBasePath.CreateFromFile(rootAssembly.CodeBase));
                        _container.Configure(cfg => cfg.For<TSettings>().Use(settings).Singleton());
                        _settingsAreInitialized = true;
                    }
                }
            }
        }
    }
}