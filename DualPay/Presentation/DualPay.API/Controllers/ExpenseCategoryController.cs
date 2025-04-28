
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/expense-categories")]
public class ExpenseCategoryController : ControllerBase
{
    private readonly IExpenseCategoryService _expenseCategoryService;
    public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
    {
        _expenseCategoryService = expenseCategoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = _expenseCategoryService.GetAll(false);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _expenseCategoryService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseCategoryRequest request)
    {
        var result = await _expenseCategoryService.AddAsync(request);
        return Ok(result);
    }
    
    [HttpPut]
    public IActionResult Create(UpdateExpenseCategoryRequest request)
    {
        var result = _expenseCategoryService.Update(request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Create([FromRoute] int id)
    {
        await _expenseCategoryService.DeleteByIdAsync(id);
        return Ok();
    }
}