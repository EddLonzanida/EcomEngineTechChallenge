using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Eml.Mef;
using Eml.MefDependencyResolver.Api;
using Eml.ClassFactory.Contracts;
using EcomEngineTechChallenge.ApiHost.Helpers;
using Newtonsoft.Json;

namespace EcomEngineTechChallenge.ApiHost
{
    public class Startup
    {
        private const string SWAGGER_DOC = "v1";

        private const string API_NAME = "EcomEngineTechChallenge";

        private const string LAUNCH_URL = "docs";
       
		public static IConfiguration Configuration { get; private set; }

        public static ILoggerFactory LoggerFactory { get; private set; }
        
		public static IClassFactory ClassFactory { get; private set; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true; 
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling= ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SWAGGER_DOC, new Info { Title = API_NAME, Version = "v1" });
                c.OperationFilter<SwashbuckleSummaryOperationFilter>();
                c.DocumentFilter<LowercaseDocumentFilter>();
            });
            ClassFactory = services.AddMef(() =>
            {
                // Register instances as shared.
                var instanceRegistrations = new List<Func<ContainerConfiguration, ExportDescriptorProvider>>
                {
                    r => r.WithInstance(LoggerFactory),
                    r => r.WithInstance(Configuration)
                };

                // Create Mef container
                return Bootstrapper.Init(API_NAME, instanceRegistrations);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            LoggerFactory.AddConsole();
            LoggerFactory.AddDebug(LogLevel.Information);
            LoggerFactory.AddNLog();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = 500;

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = LoggerFactory.CreateLogger("Global exception logger");
                        logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);

                        #if DEBUG
                            await context.Response.WriteAsync(exceptionHandlerFeature?.Error.Message);
                        #else
						    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                        #endif
                    }
                    else
                    {
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    }
                });
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SWAGGER_DOC}/swagger.json", API_NAME);
                c.RoutePrefix = LAUNCH_URL;
            });

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:2764")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
            app.UseMvc();
        }
    }
}

