using DualPay.Application.Abstraction.Token;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DualPay.Application.Features.Commands;

public class LoginAppUserCommandHandler:IRequestHandler<LoginAppUserRequest,ApiResponse<Token>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginAppUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<ApiResponse<Token>> Handle(LoginAppUserRequest request, CancellationToken cancellationToken)
    {
       AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);            
        if (user == null)
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        
        if (user == null)
        {
            throw new Exception();
        }
        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        ApiResponse<Token> response = new() { Success = signInResult.Succeeded};
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

public class LoginAppUserRequest:IRequest<ApiResponse<Token>>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}

public class LoginAppUserResponse
{
    public Token Token { get; set; }
}