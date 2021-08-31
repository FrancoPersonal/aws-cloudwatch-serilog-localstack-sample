using Serilog.Events;
using System.Collections.Generic;

namespace CloudwatchLogger
{
    public static class LogLevelMapper
    {
        public static Dictionary<LogEventLevel, string> Levels => new Dictionary<LogEventLevel, string>
            {
                { LogEventLevel.Debug, "debug" },
                { LogEventLevel.Information, "info" },
                { LogEventLevel.Warning, "warning" },
                { LogEventLevel.Error, "error" },
                { LogEventLevel.Fatal, "critical" },
                { LogEventLevel.Verbose, "debug" }
            };

        public static string Map(this LogEventLevel Level)
        {
            return Levels[Level];
        }
    }
}
