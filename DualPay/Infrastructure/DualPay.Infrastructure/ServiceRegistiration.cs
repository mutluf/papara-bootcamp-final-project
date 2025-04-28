using DualPay.Application.Abstraction.Token;
using DualPay.Infrastructure.Seeders;
using DualPay.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Infrastructure;

public static class ServiceRegistiration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ITokenHandler), typeof(TokenHandler));
        services.AddScoped<IdentitySeeder>();
    }
    
    public static async Task UseIdentitySeederAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
        await seeder.SeedAsync();
    }
}
