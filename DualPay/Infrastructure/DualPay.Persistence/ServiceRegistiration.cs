using DualPay.Application.Abstraction;
using DualPay.Domain.Entities.Identity;
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
        
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1; 
            options.Password.RequiredUniqueChars = 0;
            options.User.RequireUniqueEmail= true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                
                
        }).AddEntityFrameworkStores<DualPayDbContext>();
    }
    

 }