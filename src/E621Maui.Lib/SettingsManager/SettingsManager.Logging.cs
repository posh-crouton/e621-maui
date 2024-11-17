using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E621Maui.Lib.SettingsManager
{
    public abstract partial class SettingsManager
    {
        private ILogger? _logger;

        /// <summary>
        ///     Registers a logger to log settings manager events.
        ///     If no loggers are registered, no logs will be recorded.
        ///     If loggers are registered multiple times, the newer will
        ///     be preferred. All events logged are at debug level.  
        /// </summary>
        public void RegisterLogging(ILogger logger)
        {
            _logger = logger;
            _logger.LogDebug("Registered logger");
        }

        public void RegisterLogging(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SettingsManager>();
            _logger.LogDebug("Registered logger");
        }
    }
}
