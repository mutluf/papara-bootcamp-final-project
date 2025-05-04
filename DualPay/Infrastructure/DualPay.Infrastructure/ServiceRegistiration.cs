using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Token;
using DualPay.Infrastructure.Messaging;
using DualPay.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Infrastructure;

public static class ServiceRegistiration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHostedService<WorkerService>();
        services.AddScoped(typeof(ITokenHandler), typeof(TokenHandler));
        services.AddScoped(typeof(IEventPublishService), typeof(RabbitMqPublishService));
    }
}
