using ExchangeRatesService.Repositories;
using ExchangeRatesService.Repositories.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ExchangeRatesService
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
            services.AddMemoryCache();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ExchangeRatesService",
                    Description = "A simple example ExchangeRatesService with the Caching Decorator Pattern",
                    Contact = new OpenApiContact
                    {
                        Name = "Adis Vojvodic",
                        Email = "adivoj@gmail.com",
                    }
                });
            });
            services.AddScoped<IOpenExchangeRates, OpenExchangeRates>();

            EnableDecorator(services);
        }

        private void EnableDecorator(IServiceCollection services)
        {
            services.Decorate<IOpenExchangeRates, ExchangeRatesCachingDecorator>();
        }

        //Configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "ExchangeRatesService");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
