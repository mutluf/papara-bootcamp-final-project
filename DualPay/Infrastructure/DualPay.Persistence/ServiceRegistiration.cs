using DualPay.Application.Abstraction;
using DualPay.Persistence.Context;
using DualPay.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DualPay.Persistence;

 public static class ServiceRegistiration
 {
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<DualPayDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
 }