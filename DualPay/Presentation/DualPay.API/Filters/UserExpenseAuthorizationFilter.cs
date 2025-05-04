using System.Security.Claims;
using DualPay.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DualPay.API.Filters;
public class UserExpenseAuthorizationFilter : IAsyncActionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IExpenseService _expenseService;
    private readonly IEmployeeService _employeeService;

    public UserExpenseAuthorizationFilter(IHttpContextAccessor httpContextAccessor, IExpenseService expenseService, IEmployeeService employeeService)
    {
        _httpContextAccessor = httpContextAccessor;
        _expenseService = expenseService;
        _employeeService = employeeService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var employees = await _employeeService.Where(e=>e.UserId == Int32.Parse(userId));
        if (employees.Count() == 0)
        {
            context.Result = new ForbidResult();
            return;
        }
        var expenseId = (int)context.ActionArguments["id"];
        
        var expense = await _expenseService.GetByIdAsync(expenseId);
        if (expense == null || expense.EmployeeId != employees[0].Id)
        {
            context.Result = new ForbidResult();
            return;
        }
        await next();
    }
}
