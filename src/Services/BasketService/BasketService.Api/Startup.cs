using BasketService.Api.Core.Application.Repository;
using BasketService.Api.Core.Application.Services;
using BasketService.Api.Extensions;
using BasketService.Api.Infrastructure;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BasketService.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService.Api", Version = "v1" });
            });

            ConfigureServicesExtension(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.RegisterWithConsul(lifetime);
        }

        private void ConfigureServicesExtension(IServiceCollection services)
        {
            services.ConfigureAuth(Configuration);
            services.ConfigureConsul(Configuration);
            services.AddSingleton(sp => sp.ConfigureRedis(Configuration));

            services.AddHttpContextAccessor();

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig()
                {
                    EventNameSuffix = "IntegrationEvent",
                    EventBusType = EventBusType.RabbitMQ,
                    ConnectionRetryCount = 5,
                    SubscriberClientAppName = "BasketService"
                };

                return EventBusFactory.Create(config, sp);
            });

        }
    }
}
