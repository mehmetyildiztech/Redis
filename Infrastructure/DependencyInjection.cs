using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

            services.AddScoped<ICacheService, RedisCacheService>();

            services.AddHttpClient<IExchangeRateProvider, FrankfurterExchangeRateProvider>(client =>
                client.BaseAddress = new Uri(configuration["Frankfurter:BaseUrl"]!));

            return services;
        }
    }
}
