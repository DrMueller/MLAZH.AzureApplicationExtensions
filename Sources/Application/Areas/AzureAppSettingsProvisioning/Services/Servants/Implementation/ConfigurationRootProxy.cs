using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Services.Servants.Implementation
{
    internal class ConfigurationRootProxy : IConfigurationRootProxy
    {
        private readonly IConfigurationRoot _configRoot;

        public ConfigurationRootProxy(IConfigurationRoot configRoot)
        {
            _configRoot = configRoot;
        }

        public IEnumerable<KeyValuePair<string, string>> GetRootEntries()
        {
            return _configRoot.AsEnumerable();
        }
    }
}