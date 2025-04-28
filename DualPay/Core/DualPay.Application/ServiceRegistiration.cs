using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Mapping;
using DualPay.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Application;

public static class ServiceRegistiration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAppUserService,AppUserService>();
        services.AddScoped<IEmployeeService,EmployeeService>();
        services.AddScoped<IExpenseService,ExpenseService>();
        services.AddScoped<IExpenseCategoryService,ExpenseCategoryService>();
        services.AddAutoMapper(typeof(GeneralMapping));
    }
}