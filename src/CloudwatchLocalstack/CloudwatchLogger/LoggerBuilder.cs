using Amazon.CloudWatchLogs;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.AwsCloudWatch;
using System;

namespace CloudwatchLogger
{
    public class LoggerBuilder
    {
        private readonly IAmazonCloudWatchLogs client;

        public LoggerBuilder(IAmazonCloudWatchLogs client)
        {
            this.client = client;
        }

        public void Configure(string logGroupName)
        {
            Configure(logGroupName, new CustomLogFormatter());
        }

        public void Configure(string logGroupName, ITextFormatter formatter)
        {
            Log.Logger = new LoggerConfiguration()
             .WriteTo.AmazonCloudWatch(BuildOptions(logGroupName, formatter), client)
             .CreateLogger();
        }

        private ICloudWatchSinkOptions BuildOptions(string logGroupName, ITextFormatter formatter)
        {
            return new CloudWatchSinkOptions()
            {
                LogGroupName = logGroupName,

                // the main formatter of the log event
                TextFormatter = formatter,

                // other defaults
                MinimumLogEventLevel = LogEventLevel.Information,
                BatchSizeLimit = 100,
                QueueSizeLimit = 10000,
                Period = TimeSpan.FromSeconds(10),
                CreateLogGroup = true,
                LogStreamNameProvider = new DefaultLogStreamProvider(),
                RetryAttempts = 5
            };
        }

    }
}
