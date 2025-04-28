using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Token;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities.Identity;
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

    public async Task<ApiResponse<object>> CreateUserAsync(CreateAppUserRequest request)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            UserName = request.UserName,
            Name = request.Name,
            Surname=request.Surname,
            Email = request.Email,
        }, request.Password);
        
        ApiResponse<object> response = new() { Success = result.Succeeded};
        if (result.Succeeded)
        {
            response.Message = "User created successfully.";
        }
        else
        {
            response.Message = "User creation failed.";
            response.Errors = result.Errors.Select(e => $"{e.Code}: {e.Description}").ToList();
        }

        return response;
    }
    public async Task<ApiResponse<object>> LoginUserAsync(LoginAppUserRequest request)
    {
        AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);            
        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        
        if (user == null)
        {
            throw new Exception();
        }
        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        ApiResponse<object> response = new() { Success = signInResult.Succeeded};
        if (signInResult.Succeeded)
        {
            Token token = await _tokenHandler.CreateTokenAsync(120, user);
            response.Data = token;
            response.Message = "Login successfully";
        }
        else
        {
            response.Message ="Username or password is incorrect.";
        }
        
        return response;
    }
}

public class CreateAppUserRequest
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

public class LoginAppUserResponse
{
    public Token Token { get; set; }
    public string Message { get; set; }
    public AppUser User { get; set; }
}