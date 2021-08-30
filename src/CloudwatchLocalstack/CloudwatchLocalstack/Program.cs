using Microsoft.Extensions.DependencyInjection;
using System;

namespace CloudwatchLocalstack
{
    internal class Program
    {
        private static ServiceProvider provider = null;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Initialize();
            CreateLogSample();
            Console.ReadKey();
        }

        private static void CreateLogSample()
        {
            CloudwatchBasic cloudwatchbasic = provider.GetService<CloudwatchBasic>();
            cloudwatchbasic.CreateLogSample("log_by_logger");
        }

        private static void Initialize()
        {
            IServiceCollection services = new ServiceCollection()
                .InitialServices(true);
            provider = services.BuildServiceProvider();
            provider = services.AddSeriloglogger("log_by_logger", provider)
                .ConfigureServices().BuildServiceProvider();
        }





    }
}
