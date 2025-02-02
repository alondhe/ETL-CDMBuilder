using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using org.ohdsi.cdm.presentation.builderwebapi.Database;
using System.Net;

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
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("10.110.1.7"));
                options.KnownProxies.Add(IPAddress.Parse("185.134.75.47"));
                options.KnownProxies.Add(IPAddress.Parse("192.168.0.107"));
                options.KnownProxies.Add(IPAddress.Parse("10.5.10.33"));
             });

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
                                        "http://localhost:9000",
                                        "http://localhost:4200",
                                        "http://192.168.0.107",
                                        "http://192.168.0.107:8080",
                                        "http://10.110.1.7:8080",
                                        "http://10.110.1.7",
                                        "http://10.5.10.33/")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddHostedService<ETLService>();
            services.AddHostedService<ConversionService>();
            services.AddHostedService<QueuedHostedService>();
            
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CDM Builder", Version = "v1" });
            });           

            DatabaseInitializer.Run(Configuration).Wait();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
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
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "CDM Builder v1");
            });
        }
    }
}