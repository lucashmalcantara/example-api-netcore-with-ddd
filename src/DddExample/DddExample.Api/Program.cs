using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace DddExample.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetMinThreads();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SetMinThreads()
        {
            var minThreadsWorker = Convert.ToInt32(Environment.GetEnvironmentVariable("THREADS_MIN_WORKER"));
            var minThreadsIo = Convert.ToInt32(Environment.GetEnvironmentVariable("THREADS_MIN_IO"));

            if (minThreadsWorker != 0 && minThreadsIo != 0)
                ThreadPool.SetMinThreads(minThreadsWorker, minThreadsIo);
        }
    }
}
