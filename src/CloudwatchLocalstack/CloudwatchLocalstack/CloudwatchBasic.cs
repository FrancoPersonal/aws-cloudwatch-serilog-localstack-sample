using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;


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


        public void TestLog()
        {
            logger.LogInformation("Initial Test");

            for (int i = 0; i < 10; i++)
            {
                logger.LogInformation($"process {i}");
            }
        }

        public void CreateLogSample(string logGroupName)
        {
            try
            {
                logger.LogInformation("Initial Processing");

                DescribeLogGroupsResponse results = cloudwatch.DescribeLogGroupsAsync().GetAwaiter().GetResult();
                System.Collections.Generic.IEnumerable<string> names = results.LogGroups.Select(log => log.LogGroupName);
                LogInfo(JsonConvert.SerializeObject(names));
                if (!names.Any(name => name == logGroupName))
                {
                    CreateLogGroupResponse response = cloudwatch.CreateLogGroupAsync(new CreateLogGroupRequest(logGroupName: logGroupName)).GetAwaiter().GetResult();
                    LogInfo($"{response.HttpStatusCode}");
                    logger.LogInformation("response {status}", response.HttpStatusCode);
                }
                logger.LogInformation("finish Processing");
                LogInfo("finish");
                if (names.Any(name => name == logGroupName))
                {
                    FilterLogEventsResponse fields = cloudwatch.FilterLogEventsAsync(new FilterLogEventsRequest { LogGroupName = logGroupName }).GetAwaiter().GetResult();
                    fields.Events.ForEach(e => Console.WriteLine(e.Message));
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
                logger.LogError("response {error}", ex);
            }
        }

        private void LogInfo(string message)
        {
            Console.WriteLine(message);
            logger.LogInformation(message);
        }
        private void LogError(string message)
        {
            Console.WriteLine(message);
            logger.LogError(message);
        }

    }
}
