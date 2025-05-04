using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using DualPay.Application.Abstraction.Services;

namespace DualPay.API.Filters;
public class AuthorizeOwnEmployeeFilter : IAsyncActionFilter
{
    private readonly IEmployeeService _employeeService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizeOwnEmployeeFilter(IHttpContextAccessor httpContextAccessor, IEmployeeService employeeService)
    {
        _httpContextAccessor = httpContextAccessor;
        _employeeService = employeeService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        if (!context.ActionArguments.TryGetValue("id", out var idObj) || idObj is not int employeeId)
        {
            context.Result = new BadRequestObjectResult("Employee ID is required.");
            return;
        }

        var employee = await _employeeService.GetByIdAsync(employeeId);
        if (employee == null)
        {
            context.Result = new NotFoundObjectResult("Employee not found.");
            return;
        }

        var isAdmin = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
        if (!isAdmin && employee.UserId.ToString() != userId)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}
