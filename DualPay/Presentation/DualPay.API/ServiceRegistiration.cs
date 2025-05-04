using DualPay.API.Filters;

namespace DualPay.API;

public static class ServiceRegistiration
{
    public static void AddAPIServices(this IServiceCollection services)
    {
        services.AddScoped<UserExpenseAuthorizationFilter>(); 
        services.AddScoped<AuthorizeOwnEmployeeFilter>();
    }
}