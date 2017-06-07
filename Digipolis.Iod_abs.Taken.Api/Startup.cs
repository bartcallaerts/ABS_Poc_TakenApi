using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Digipolis.Iod_abs.Taken.Common;
using Digipolis.Iod_abs.Taken.Manager.Interface;
using Digipolis.Iod_abs.Taken.Manager.Manager;
using Digipolis.Iod_abs.Taken.Service.Interface;
using Digipolis.Iod_abs.Taken.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Narato.Common.Exceptions;
using Narato.Common.Factory;
using Narato.Common.Interfaces;
using Narato.Common.Mappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;

namespace Digipolis.Iod_abs.Taken.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.Configure<ApiConfiguration>(Configuration.GetSection("ApiConfiguration"));
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Taken API",
                        Version = "v1",
                        Description = "Taken API for ABS POC 1"
                    }
                );
            });

            var apiConfigurationSection = Configuration.GetSection("ApiConfiguration");
            var workflowConfiguration = apiConfigurationSection.GetSection("WorkflowEngineConfig");
            var apiKey = workflowConfiguration.GetValue<Guid>("ApiKey");
            var baseUrl = workflowConfiguration.GetValue<string>("BaseUrl");

            services.AddMvc().AddJsonOptions(x =>
                    {
                        x.SerializerSettings.ContractResolver =
                            new CamelCasePropertyNamesContractResolver();
                        x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    }
                );
            services.AddCors();


            services.AddScoped<IResponseFactory, ResponseFactory>();
            services.AddScoped<ITaskManager, TaskManager>();
            services.AddScoped<IExceptionHandler, ExceptionHandler>();
            services.AddScoped<IProcessInstanceManager, ProcessInstanceManager>();
            services.AddScoped<IExceptionToActionResultMapper, ExceptionToActionResultMapper>();
            services.AddTransient<IWorkflowService, WorkFlowService>(c =>
            {
                var options = c.GetService<IOptions<ApiConfiguration>>();
                return new WorkFlowService(GetWorkflowHttpClient(baseUrl, apiKey), options);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
        }

        private HttpClient GetWorkflowHttpClient(string baseAddress, Guid apiKey)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            httpClient.DefaultRequestHeaders.Add("apikey", apiKey.ToString());

            return httpClient;
        }
    }
}
