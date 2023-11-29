using CatalogService.Api.Extensions;
using CatalogService.Api.Infrastructure.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CatalogService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);

            // �lk ba�ta seed migrationu basar.
            hostBuilder.MigrateDbContext<CatalogContext>((context, services) =>
            {
                var env = services.GetRequiredService<IWebHostEnvironment>();
                var logger = services.GetRequiredService<ILogger<CatalogContextSeed>>();

                new CatalogContextSeed()
                .SeedAsync(context, env, logger)
                .Wait();

            });

            hostBuilder.Run();

        }

        /// <summary>
        /// Catalog i�lemler i�in Pics kals�r�ndeki dosyalara eri�ir.Bu klas�rde sat�lacak �r�nlerin fotolar vard�r. 
        /// �lk ba�ta catalog servisinde buras� �al��mal�.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateOnBuild = false;
                options.ValidateScopes = false;
            })
            .UseStartup<Startup>()
            .UseWebRoot("Pics")
            .UseContentRoot(Directory.GetCurrentDirectory())
            .Build();

    }
}
