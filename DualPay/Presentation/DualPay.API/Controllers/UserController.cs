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
    
    /// <summary>
    /// INITIAL ADMIN LOGIN
    /// {
    ///        "usernameOrEmail": "dpadmin@dualpay.com.tr",
    ///         "password": "DP2025!"
    /// }
    ///
    /// INITIAL PERSONNEL LOGIN
    ///  {
    ///  "usernameOrEmail": "dpuser@dualpay.com.tr",
    /// "password": "DP2025!"
    /// }
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginAppUserRequest request)
    {
        Token token = await _appUserService.LoginUserAsync(request);
        return Ok(token);
    }
}