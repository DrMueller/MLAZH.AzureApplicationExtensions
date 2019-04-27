using System;
using System.Collections.Generic;
using System.Text;

namespace Mmu.Mlazh.AzureApplicationExtensions.Areas.Logging
{
    public interface ILoggingService
    {
        void LogError(Exception ex);

        void LogInformation(string message);
    }
}
