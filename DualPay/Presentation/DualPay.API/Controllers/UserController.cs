using DualPay.Application.Abstraction;
using DualPay.Application.DTOs;
using DualPay.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IAppUserService _appUserService;

    public UserController(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser(CreateAppUserRequest request)
    {
        await _appUserService.CreateUserAsync(request);
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAppUserRequest request)
    {
        Token token = await _appUserService.LoginUserAsync(request);
        return Ok(token);
    }
}