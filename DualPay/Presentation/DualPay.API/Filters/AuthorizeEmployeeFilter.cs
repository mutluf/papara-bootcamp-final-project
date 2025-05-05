using System.Security.Claims;
using DualPay.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DualPay.API.Filters;
public class AuthorizeEmployeeFilter : IAsyncActionFilter
{
    private readonly IEmployeeService _employeeService;

    public AuthorizeEmployeeFilter(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        if (user.IsInRole("Admin"))
        {
            await next();
            return;
        }
        
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!context.RouteData.Values.TryGetValue("EmployeeId", out var employeeIdRouteValue) || employeeIdRouteValue == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var employees = await _employeeService.Where(e=>e.UserId == Int32.Parse(userId));
        if (employees.Count > 0 && employeeIdRouteValue.ToString() != employees[0].Id.ToString())
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}