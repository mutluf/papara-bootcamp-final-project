using System.Net;
using DualPay.Application.Abstraction;
using DualPay.Application.Common.Models;
using DualPay.Application.Services;
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
    public async Task<IActionResult> CreateUser(CreateAppUserRequest request)
    {
        var apiResponse = await _appUserService.CreateUserAsync(request);
        return Ok(apiResponse);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAppUserRequest request)
    {
        ApiResponse<object> response = await _appUserService.LoginUserAsync(request);
        return Ok(response);
    }
}