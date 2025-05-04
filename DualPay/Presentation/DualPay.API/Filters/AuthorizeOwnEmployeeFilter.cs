using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using DualPay.Application.Abstraction.Services;

namespace DualPay.API.Filters;
public class AuthorizeOwnEmployeeFilter : IAsyncActionFilter
{
    private readonly IEmployeeService _employeeService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IExpenseService _expenseService;

    public AuthorizeOwnEmployeeFilter(IHttpContextAccessor httpContextAccessor, IEmployeeService employeeService, IExpenseService expenseService)
    {
        _httpContextAccessor = httpContextAccessor;
        _employeeService = employeeService;
        _expenseService = expenseService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var isAdmin = _httpContextAccessor.HttpContext.User.IsInRole("Admin");
        var employees = await _employeeService.Where(e=>e.UserId == Int32.Parse(userId));
        if (isAdmin)
        {
            await next();
            return;
        }
      
        if (!employees.Any())
        {
            context.Result = new NotFoundObjectResult("Employee not found.");
            return;
        }
    
        var expenseId = (int)context.ActionArguments["id"];
        var expense = await _expenseService.GetByIdAsync(expenseId);
        if (expense == null || employees.Count() > 0 && expense?.EmployeeId != employees[0].Id)
        {
            context.Result = new ForbidResult();
            return;
        }
        await next();
    }
}
