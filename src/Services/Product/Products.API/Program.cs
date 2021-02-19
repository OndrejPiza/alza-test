using System;
using System.Threading;
using System.Threading.Tasks;
using AlzaTest.Services.Utils.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AlzaTest.Services.Products.API
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information();

            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Information("Starting web host");

                await CreateHostBuilder(args)
                    .Build()
                    .RunWithTasksAsync(CancellationToken.None);

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
