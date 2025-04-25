using DualPay.Application.Abstraction;
using DualPay.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Application;

public static class ServiceRegistiration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
    }
}