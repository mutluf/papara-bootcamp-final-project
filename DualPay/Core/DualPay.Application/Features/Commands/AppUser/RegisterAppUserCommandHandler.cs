using DualPay.Application.Common.Models;
using DualPay.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DualPay.Application.Features.Commands;

public class RegisterAppUserCommandHandler:IRequestHandler<RegisterAppUserCommandRequest,ApiResponse>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterAppUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse> Handle(RegisterAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            UserName = request.UserName,
            Name = request.Name,
            Surname=request.Surname,
            Email = request.Email,
        }, request.Password);
        
        ApiResponse response = new() { Success = result.Succeeded};
        if (result.Succeeded)
        {
            response.Message = "User created successfully.";
        }
        else
        {
            response.Message = "User creation failed.";
        }

        return response;
    }
}

public class RegisterAppUserCommandRequest :  IRequest<ApiResponse>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}