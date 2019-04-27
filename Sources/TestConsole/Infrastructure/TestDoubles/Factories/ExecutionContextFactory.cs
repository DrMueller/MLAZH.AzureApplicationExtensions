using System;
using System.IO;
using System.Reflection;
using Microsoft.Azure.WebJobs;

namespace Mmu.Mlazh.AzureApplicationExtensions.TestConsole.Infrastructure.ExecutionContexts.Services.Implementation
{
    public static class ExecutionContextFactory
    {
        public static ExecutionContext CreateDefault()
        {
            var context = new ExecutionContext
            {
                InvocationId = Guid.NewGuid(),
                FunctionAppDirectory = GetExecutingAssemblyDirectory()
            };

            return context;
        }

        private static string GetExecutingAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var uriPath = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(uriPath);
        }
    }
}