using Amazon.CloudWatchLogs;
using CloudwatchLogger;
using LocalstackBridge.Abstractions;
using LocalstackBridge.Cloudwatch;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CloudwatchLocalstack
{
    public static class StartUp
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<CloudwatchBasic>();
            return services;
        }
        public static IServiceCollection InitialServices(this IServiceCollection services, bool isLocal = false, LocalCredentials credentials = null)
        {
            services.AddSingleton(typeof(IAmazonCloudWatchLogs),
                isLocal ? LocalCloudWatchFactory.BuildInstance(credentials) : new AmazonCloudWatchLogsClient());
            return services;
        }

        public static IServiceCollection AddSeriloglogger(this IServiceCollection services, string logGroupName, ServiceProvider provider = null)
        {
            services.AddSingleton<LoggerBuilder>();
            provider = services.BuildServiceProvider();
            LoggerBuilder logger = provider.GetService<LoggerBuilder>();
            logger.Configure(logGroupName);
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog()
                );
            provider = services.BuildServiceProvider();
            return services;
        }

    }
}
