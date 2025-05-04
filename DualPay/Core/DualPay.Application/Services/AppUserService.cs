using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Token;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DualPay.Application.Services;
public class AppUserService : IAppUserService
{
    readonly UserManager<AppUser> _userManager;
    readonly SignInManager<AppUser> _signInManager;
    readonly ITokenHandler _tokenHandler;

    public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task CreateUserAsync(CreateAppUserRequest request)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            UserName = request.UserName,
            Name = request.Name,
            Surname=request.Surname,
            Email = request.Email,
        }, request.Password);
    }
    public async Task<Token> LoginUserAsync(LoginAppUserRequest request)
    {
        AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);            
        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        
        if (user == null)
        {
            throw new Exception();
        }
        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (signInResult.Succeeded)
        {
            Token token = await _tokenHandler.CreateTokenAsync(120, user);
            return token;
        }

        return new Token();
    }
}

public class CreateAppUserRequest :IRequest<Token>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginAppUserRequest
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
