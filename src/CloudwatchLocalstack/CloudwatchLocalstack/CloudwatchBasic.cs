using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Microsoft.Extensions.Logging;
using System;

namespace CloudwatchLocalstack
{
    public class CloudwatchBasic
    {
        private readonly IAmazonCloudWatchLogs cloudwatch;
        private readonly ILogger logger;
        public CloudwatchBasic(IAmazonCloudWatchLogs cloudwatch, ILogger<CloudwatchBasic> logger)
        {
            this.cloudwatch = cloudwatch;
            this.logger = logger;
        }

        public void CreateLogSample()
        {
            try
            {
                CreateLogGroupRequest request = new()
                { LogGroupName = "Test" };
                logger.LogInformation("Initial");
                CreateLogGroupResponse response = cloudwatch.CreateLogGroupAsync(request).GetAwaiter().GetResult();
                Console.WriteLine(response.HttpStatusCode);
                logger.LogInformation("response {status}", response.HttpStatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.LogError("response {error}", ex);
            }
        }
    }
}
