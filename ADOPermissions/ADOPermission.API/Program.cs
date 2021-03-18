using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ADOPermission.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .UseMetricsWebTracking()   
                 .UseMetrics( options =>
                 {
                     options.EndpointOptions = endpointOptions =>
                     {
                         endpointOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                         endpointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                         endpointOptions.EnvironmentInfoEndpointEnabled = false;
                     };
                 })
                 .ConfigureServices((context, services) =>
                 {
                     services.Configure<KestrelServerOptions>(
                         context.Configuration.GetSection("Kestrel"));
                 }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
                //.ConfigureLogging( logginng =>
                //{
                //    logginng.ClearProviders();
                //    logginng.AddDebug();
                //    logginng.AddConsole();
                //}                );
    }
}
