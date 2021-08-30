using Microsoft.Extensions.DependencyInjection;
using System;

namespace CloudwatchLocalstack
{
    internal class Program
    {
        private static ServiceProvider provider = null;
        private static CloudwatchBasic cloudwatchbasic;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string log = $"log{DateTime.Now.ToString("yyyyMMdd")}";
            Initialize(log);
            cloudwatchbasic = provider.GetService<CloudwatchBasic>();
            cloudwatchbasic.TestLog();
            cloudwatchbasic.CreateLogSample(log);
            Console.ReadKey();
        }

        private static void Initialize(string logName)
        {
            IServiceCollection services = new ServiceCollection()
                .InitialServices(true);
            provider = services.BuildServiceProvider();
            provider = services.AddSeriloglogger(logName, provider)
                .ConfigureServices().BuildServiceProvider();
        }





    }
}
