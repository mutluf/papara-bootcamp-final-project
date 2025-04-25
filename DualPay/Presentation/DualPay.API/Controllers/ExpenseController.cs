using DualPay.Application.Abstraction;
using DualPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpenseController : ControllerBase
{
    private readonly IGenericService<Expense> _expenseService;

    public ExpenseController(IGenericService<Expense> expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _expenseService.GetAllAsync();
        return Ok(result);
    }
}