using System.Security.Claims;
using DualPay.API.Attributes;
using DualPay.Application.Common.Models;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseResponse = DualPay.Application.Features.Queries.ExpenseResponse;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllExpensesQueryRequest request)
    {
        ApiResponse<List<ExpenseResponse>> result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    [UserExpenseAuthorization]
    public async Task<IActionResult> GetById([FromRoute] GetExpenseByIdRequest  request, [FromRoute] int id)
    {
        request.Id = id;
        ApiResponse<ExpenseDetailResponse> result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create([FromBody] CreateExpenseCommandRequest request)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new ApiResponse("Unauthorized."));
        }
        request.UserId = Int32.Parse(userId);
        ApiResponse<Application.Features.Commands.ExpenseCategories.ExpenseResponse> apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    [HttpPut("{id}")]
    [UserExpenseAuthorization]
    public async Task<IActionResult> Update([FromBody] UpdateExpenseCommandRequest request,  [FromRoute] int id)
    {
        request.Id = id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    [HttpDelete("{id}")]
    [UserExpenseAuthorization]
    public async Task<IActionResult> Delete([FromRoute] DeleteExpenseCategoryCommandRequest request, [FromRoute] int id)
    {
        request.Id = id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
}