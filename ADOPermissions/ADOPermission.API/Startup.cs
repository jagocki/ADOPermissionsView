using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADOPermission.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PactoTrace;
using PactoTrace.Core;

namespace ADOPermission.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Unit.Scope(() =>
            {
                services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
                services.AddRazorPages();
                services.AddLogging( (logger) =>
                {
                    logger.AddConsole((options) =>
                   {
                       options.IncludeScopes = true;
                   });
                });
                services.AddMetrics();
                //services.AddTransient<PactoTraceCore>();
                services.AddSingleton(typeof(IGenericEventSink), typeof(PactoTraceCore));
                services.AddTransient<UsersService>();
                services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            Unit.Scope(() => PerformConfiguration(app, env));
        }

        private static void PerformConfiguration(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
