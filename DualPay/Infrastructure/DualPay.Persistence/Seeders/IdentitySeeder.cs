using DualPay.Application.Abstraction;
using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DualPay.Infrastructure.Seeders;

public class IdentitySeeder
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    public IdentitySeeder(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync()
    {
        await CheckRolesExistAsync();
        await CheckAdminExistsAsync();
        await CheckInitialUserExistsAsync();
    }

    private async Task CheckRolesExistAsync()
    {
        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var appRole = new AppRole { Name = role };
                var result = await _roleManager.CreateAsync(appRole);
                if (result.Succeeded)
                    Console.WriteLine($"Role '{role}' created.");
                else
                    Console.WriteLine($"Failed to create role '{role}'.");
            }
        }
    }

    private async Task CheckAdminExistsAsync()
    {
        var email = _configuration["InitialAdmin:Email"];
        var password = _configuration["InitialAdmin:Password"];

        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
            return;

        var user = new AppUser
        {
            Name = "Admin",
            Surname = "Initial",
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = "1555555555",
            AccountNumber = "1111111111",
            Balance = 100000
        };

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            Console.WriteLine("Admin user created and added to Admin role.");
        }
        else
        {
            Console.WriteLine("Failed to create admin user:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.Description}");
            }
        }
    }

    private async Task CheckInitialUserExistsAsync()
    {
        var email = _configuration["InitialUser:Email"];
        var password = _configuration["InitialUser:Password"];

        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser != null)
            return;

        var user = new AppUser
        {
            Name = "Employee",
            Surname = "Initial",
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = "5555555555",
            AccountNumber = "2222222222",
            Balance = 2000
        };

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            Console.WriteLine("Initial user created and added to User role.");

            var employee = new Employee
            {
                UserId = user.Id,
                IdentityNumber = "12345678912",
                PhoneNumber = "5555555555",
                AccountNumber = "2222222222",
            };

            await _unitOfWork.GetRepository<Employee>().AddAsync(employee);
            await _unitOfWork.Complete();
            Console.WriteLine("Employee Created..");
        }
        else
        {
            Console.WriteLine("Failed to create initial user:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.Description}");
            }
        }
    }

}
