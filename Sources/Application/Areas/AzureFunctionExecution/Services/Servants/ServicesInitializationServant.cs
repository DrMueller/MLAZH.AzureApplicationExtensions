using System;
using System.Reflection;
using Mmu.Mlh.ApplicationExtensions.Areas.DependencyInjection.Models;
using Mmu.Mlh.ApplicationExtensions.Areas.DependencyInjection.Services;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureFunctionExecution.Services.Servants
{
    internal static class ServicesInitializationServant
    {
        private static bool _isInitialized;
        private static object _lock = new object();

        internal static void AssureServicesAreInitialized(Assembly rootAssembly, Action provideDependenciesCallback = null)
        {
            if (!_isInitialized)
            {
                lock (_lock)
                {
                    if (!_isInitialized)
                    {
                        provideDependenciesCallback?.Invoke();
                        ContainerInitializationService.CreateInitializedContainer(new AssemblyParameters(rootAssembly, "Mmu"));
                        _isInitialized = true;
                    }
                }
            }
        }
    }
}