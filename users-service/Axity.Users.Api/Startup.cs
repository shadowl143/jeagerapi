// <summary>
// <copyright file="Startup.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Api
{
    using Axity.Users.Api.Filter;
    using Axity.Users.DependicyInjection;
    using Axity.Users.Resources.Util;
    using Company.Proyect.Api.Filter;
    using Microsoft.Extensions.DependencyInjection;
    using OpenTelemetry;
    using OpenTelemetry.Resources;
    using OpenTelemetry.Trace;
    using Serilog;
    using Serilog.Context;
    using Serilog.Events;
    using StackExchange.Redis;
    using System.Diagnostics;

    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        private const string AXITYURL = "https://www.axity.com/";

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">(IConfiguration configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        /// <value>Configuration.</value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// configuration service.
        /// </summary>
        /// <param name="services">IServiceCollection service.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S2325:Methods and properties that don't access instance data should be static", Justification = "Se utiliza para la inyeccion d depencias")]
        public void ConfigureServices(IServiceCollection services)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(new EnvironmentVariableLoggingLevelSwitch(Environment.GetEnvironmentVariable("SEQ_LOG_LEVEL")))
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .Enrich.WithProperty("Enviroment", this.Configuration["ElasticApm:Environment"])
                .Enrich.WithProperty("Application", this.Configuration["ApplicationName"])
                .Destructure.ToMaximumCollectionCount(16)
                .Destructure.ToMaximumStringLength(50)
                .Destructure.ToMaximumDepth(5)
                .WriteTo.File("Logs/log-.txt", outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] - {Message:lj}{NewLine} {Exception}", rollingInterval: RollingInterval.Day);

            Log.Logger = loggerConfiguration.CreateLogger();
            ILoggerFactory loggerFactory = new LoggerFactory();
            services.AddSingleton(loggerFactory);
            services.AddSingleton(Log.Logger);

            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddMvcOptions(p => p.Filters.Add(new CustomActionFilterAttribute(Log.Logger)));
            mvcBuilder.AddMvcOptions(p => p.Filters.Add(new CustomExceptionFilterAttribute(Log.Logger)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api Student",
                    Description = "Api para información de estudiantes",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Axity",
                        Url = new System.Uri(AXITYURL),
                    },
                });
            });
            services.AddOpenTelemetry()
            .WithTracing(builder => builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(this.Configuration.GetSection("ApplicationName").Value?.ToString() ?? "api-service"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(this.Configuration.GetSection("NombreJager").Value?.ToString() ?? "http://localhost:4317"); // Jaeger OTLP endpoint
                    options.Endpoint = new Uri(this.Configuration.GetSection("NombreJager").Value?.ToString() ?? "http://loki:3100"); // Jaeger OTLP endpoint
                }));
            this.AddRedis(services, Log.Logger);
            services.AddDependicyInjection();
            services.AddAutoMapper();
            services.AddDbContext(this.Configuration);
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configure.
        /// </summary>
        /// <param name="app">WebApplication app.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S2325:Methods and properties that don't access instance data should be static", Justification = "Necesita reflejar cambios futuros")]
        public void Configure(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api student");
                c.RoutePrefix = string.Empty;
            });

            // Middleware para adjuntar TraceId y SpanId a los logs de Serilog
            app.Use(async (context, next) =>
            {
                var activity = Activity.Current;
                if (activity != null)
                {
                    using (LogContext.PushProperty("TraceId", activity.TraceId.ToString()))
                    using (LogContext.PushProperty("SpanId", activity.SpanId.ToString()))
                    {
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseMiddleware<ResponseMiddleware>();
        }

        /// <summary>
        /// Add configuration Redis.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <param name="logger">The logger.</param>
        private void AddRedis(IServiceCollection services, Serilog.ILogger logger)
        {
            try
            {
                var configuration = ConfigurationOptions.Parse(this.Configuration["redis:hostname"], true);
                configuration.ResolveDns = true;

                ConnectionMultiplexer cm = ConnectionMultiplexer.Connect(configuration);
                services.AddSingleton<IConnectionMultiplexer>(cm);
            }
            catch (Exception)
            {
                logger.Error("No se econtro Redis");
            }
        }
    }
}
