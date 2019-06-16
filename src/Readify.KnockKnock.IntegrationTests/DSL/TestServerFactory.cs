using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Readify.KnockKnock.Api.Services;

namespace Readify.KnockKnock.IntegrationTests.DSL
{
    public static class TestServerFactory
    {
        public static TestServer CreateTestServer()
        {
            var builder = WebHost
                .CreateDefaultBuilder()
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                })
                .ConfigureServices(services =>
                {
                    services
                        .AddControllers()
                        .AddNewtonsoftJson();

                    services.AddSingleton<IFibonacciService, FibonacciService>();
                    services.AddSingleton<IWordsService, WordsService>();
                    services.AddSingleton<ITriangleService, TriangleService>();
                });

            var server = new TestServer(builder);
            return server;
        }

    }
}
