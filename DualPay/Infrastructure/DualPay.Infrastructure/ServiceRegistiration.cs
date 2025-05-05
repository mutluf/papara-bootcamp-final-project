using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Token;
using DualPay.Infrastructure.Caching;
using DualPay.Infrastructure.Messaging;
using DualPay.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Infrastructure;

public static class ServiceRegistiration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfiguration = configuration.GetSection("Redis");

        services.AddHostedService<WorkerService>();
        services.AddScoped(typeof(ITokenHandler), typeof(TokenHandler));
        services.AddScoped(typeof(IEventPublishService), typeof(RabbitMqPublishService));
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfiguration["ConnectionString"] ?? "localhost:6379";
            options.InstanceName = redisConfiguration["InstanceName"] ?? "Reports_";
        });
        services.AddScoped(typeof(ICacheService), typeof(RedisCacheService));
    }
}
