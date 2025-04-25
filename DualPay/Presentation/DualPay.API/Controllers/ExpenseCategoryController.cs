using DualPay.Application.Abstraction;
using DualPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/expense-categories")]
public class ExpenseCategoryController : Controller
{
    private readonly IGenericService<ExpenseCategory> _expenseCategoryService;

    public ExpenseCategoryController(IGenericService<ExpenseCategory> expenseService)
    {
        _expenseCategoryService = expenseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _expenseCategoryService.GetAllAsync();
        return Ok(result);
    }
}