﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;

namespace CatalogService.Api.Extensions
{
    /// <summary>
    /// Catalog servisi ayağa kaldırınca migrationları bassın diye yazıldı. Seed migrationu basar.
    /// </summary>
    public static class HostExtension
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<TContext>>();

                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    //Policy bu retry mekanizması için kullanılır. DB ye bağlanmayı dener. 3-5-8 sn boyunca.
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(new TimeSpan[]
                        {
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(8),
                        });

                    retry.Execute(() => InvokeSeeder(seeder, context, services));
                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                }
            }

            return host;
        }

        /// <summary>
        /// database migrationları basar.Database.Migrate() eder.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="seeder"></param>
        /// <param name="context"></param>
        /// <param name="services"></param>
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
