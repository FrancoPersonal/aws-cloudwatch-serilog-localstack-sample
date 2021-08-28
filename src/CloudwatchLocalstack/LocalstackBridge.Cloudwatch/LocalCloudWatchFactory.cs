using Amazon.CloudWatchLogs;
using LocalstackBridge.Abstractions;

namespace LocalstackBridge.Cloudwatch
{
    public static class LocalCloudWatchFactory
    {
        public static IAmazonCloudWatchLogs BuildInstance(LocalCredentials? credentials = null)
        {
            LocalCredentials awscredentials = credentials ?? new LocalCredentials();
            return new AmazonCloudWatchLogsClient(
                awscredentials.AwsAccessKeyId,
                awscredentials.AwsSecretAccessKey,
                new AmazonCloudWatchLogsConfig()
                {
                    ServiceURL = awscredentials.ServiceURL
                });
        }
    }
}
