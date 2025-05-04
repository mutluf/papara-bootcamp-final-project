using System.Reflection;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Behaviours;
using DualPay.Application.Mapping;
using DualPay.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Application;
public static class ServiceRegistiration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAppUserService,AppUserService>();
        services.AddScoped<IEmployeeService,EmployeeService>();
        services.AddScoped<IExpenseService,ExpenseService>();
        services.AddScoped<IReportService,ReportService>();
        services.AddScoped<IExpenseCategoryService,ExpenseCategoryService>();
        services.AddAutoMapper(typeof(GeneralMapping));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GeneralMapping).Assembly));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}