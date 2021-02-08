using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using org.ohdsi.cdm.presentation.builderwebapi.Hubs;
using System;

namespace org.ohdsi.cdm.presentation.builderwebapi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins("http://cdmwizard.arcadialab.ru",
                                        "http://185.134.75.47", 
                                        "http://185.134.75.47:9000", 
                                        "http://cdmwizard.arcadialab.ru:9000",
                                        "http://localhost:9000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            if (string.Equals(
    Environment.GetEnvironmentVariable("ASPNETCORE_FORWARDEDHEADERS_ENABLED"),
    "true", StringComparison.OrdinalIgnoreCase))
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                        ForwardedHeaders.XForwardedProto;
                    // Only loopback proxies are allowed by default.
                    // Clear that restriction because forwarders are enabled by explicit 
                    // configuration.
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                });
            }

            services.AddControllers();
            services.AddSignalR().AddHubOptions<LogHub>(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(MyAllowSpecificOrigins);

            //app.UseCors(builder => builder
            //    .AllowAnyHeader()
            //    .AllowAnyMethod()
            //    .SetIsOriginAllowed((host) => true)
            //    .AllowCredentials()
            //);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LogHub>("/log");
                endpoints.MapControllers();
            });
        }
    }
}
