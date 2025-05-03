using DualPay.Application.Abstraction.Token;
using DualPay.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Infrastructure;

public static class ServiceRegistiration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ITokenHandler), typeof(TokenHandler));
    }
}
