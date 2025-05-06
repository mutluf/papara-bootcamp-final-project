using DualPay.Application.Common.Models;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;
[ApiController]
[Route("api/expense-categories")]
[Authorize(Roles = "Admin")]
public class ExpenseCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        GetAllExpenseCategoriesRequest request = new GetAllExpenseCategoriesRequest();
        ApiResponse<List<ExpenseCategoryResponse>> result = await _mediator.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetExpenseCategoryByIdRequest request = new GetExpenseCategoryByIdRequest();
        request.Id = id;
        ApiResponse<ExpenseCategoryResponse> result =await _mediator.Send(request);
        return Ok(result);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateExpenseCategoryCommandRequest request)
    {
        ApiResponse<ExpenseCategoryResponse> apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateExpenseCategoryCommandRequest request, [FromRoute] int id)
    {
        request.Id = id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteExpenseCategoryCommandRequest request = new DeleteExpenseCategoryCommandRequest();
        request.Id = id;
        ApiResponse apiResponse= await _mediator.Send(request);
        return Ok(apiResponse);
    }
}