using DualPay.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DualPay.Infrastructure.Seeders;

public class IdentitySeeder
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;

    public IdentitySeeder(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
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
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = ""
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
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = ""
        };

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            Console.WriteLine("Initial user created and added to User role.");
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
