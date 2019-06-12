using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Readify.KnockKnock.Api.Configuration;
using Readify.KnockKnock.Api.Services;

namespace Readify.KnockKnock.Api
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            var configuration = Configuration.Get<EnvironmentConfiguration>();
            app.UseSwagger(option => { option.RouteTemplate = configuration.Swagger.JsonRoute; });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(configuration.Swagger.UiEndpoint, configuration.Swagger.Description);
            });
        }
    }
}
