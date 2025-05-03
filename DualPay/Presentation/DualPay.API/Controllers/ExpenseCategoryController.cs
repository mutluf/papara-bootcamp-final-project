using DualPay.Application.Common.Models;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpenseResponse = DualPay.Application.Features.Queries.ExpenseResponse;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/expense-categories")]
public class ExpenseCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    //[Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetAll(GetAllExpensesQueryRequest request)
    {
        var publishService = new PublishService();
        await publishService.Publish(new EventDto() {Id = 1});
        ApiResponse<List<ExpenseResponse>> result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] GetExpenseCategoryByIdRequest request)
    {
        ApiResponse<ExpenseCategoryResponse> result =await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateExpenseCategoryCommandRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateExpenseCategoryCommandRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteExpenseCategoryCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}