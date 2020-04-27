using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Readify.KnockKnock.Configuration;
using Readify.KnockKnock.Middlewares;
using Readify.KnockKnock.Services;

namespace Readify.KnockKnock
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
            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddResponseCaching();

            services.AddTransient<RequestResponseLoggingMiddleware>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KnockKnock Readify API",
                    Version = "v1",
                });
            });

            services.AddSingleton<IFibonacciService, FibonacciService>();
            services.AddSingleton<IWordsService, WordsService>();
            services.AddSingleton<ITriangleService, TriangleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(30)
                    };

                await next();
            });

            var configuration = Configuration.Get<EnvironmentConfiguration>();
            app.UseSwagger(option => { option.RouteTemplate = configuration.Swagger.JsonRoute; });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(configuration.Swagger.UiEndpoint, configuration.Swagger.Description);
            });
        }
    }
}
