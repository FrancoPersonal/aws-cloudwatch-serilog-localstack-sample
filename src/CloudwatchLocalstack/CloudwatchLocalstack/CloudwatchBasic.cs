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

        public void CreateLogSample(string logGroupName)
        {
            try
            {
                logger.LogInformation("Initial");

                DescribeLogGroupsResponse results = cloudwatch.DescribeLogGroupsAsync().GetAwaiter().GetResult();
                System.Collections.Generic.IEnumerable<string> names = results.LogGroups.Select(log => log.LogGroupName);
                Console.WriteLine(JsonConvert.SerializeObject(names));
                if (!names.Any(name => name == logGroupName))
                {
                    CreateLogGroupResponse response = cloudwatch.CreateLogGroupAsync(new CreateLogGroupRequest(logGroupName: logGroupName)).GetAwaiter().GetResult();
                    Console.WriteLine(response.HttpStatusCode);
                    logger.LogInformation("response {status}", response.HttpStatusCode);
                }
                else
                {
                    FilterLogEventsResponse fields = cloudwatch.FilterLogEventsAsync(new FilterLogEventsRequest { LogGroupName = logGroupName }).GetAwaiter().GetResult();

                    fields.Events.ForEach(e => Console.WriteLine(e.Message));
                }
                Console.WriteLine("finish");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.LogError("response {error}", ex);
            }
        }
    }
}
