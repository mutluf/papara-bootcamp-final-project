using System.Security.Claims;
using DualPay.API.Attributes;
using DualPay.Application.Common.Models;
using DualPay.Application.Features.Commands.Expense;
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
    public async Task<IActionResult> GetAll()
    {
        GetAllExpensesQueryRequest request = new GetAllExpensesQueryRequest();
        ApiResponse<List<ExpenseResponse>> result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    [AuthorizeEmployeeForOwnExpense]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetExpenseByIdRequest request = new GetExpenseByIdRequest();
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
        ApiResponse<ExpenseResponse> apiResponse = await _mediator.Send(request);
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
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteExpenseCategoryCommandRequest request = new DeleteExpenseCategoryCommandRequest();
        request.Id = id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    [HttpPost("submit/{id}")]
    [UserExpenseAuthorization]
    public async Task<IActionResult> SendExpenseToApproval([FromBody] SendExpenseToApprovalCommandRequest request, [FromRoute] int id)
    {
        request.ExpenseId = id;
        ApiResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    [HttpPost("approve/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveExpenseAsync([FromBody] ApproveExpenseCommandRequest request, [FromRoute] int id)
    {
        request.ExpenseId = id;
        ApiResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    
    [HttpPost("reject/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectExpenseAsync([FromBody] RejectExpenseCommandRequest request, [FromRoute] int id)
    {
        request.ExpenseId = id;
        ApiResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredExpenses([FromQuery] string[] filters, [FromQuery] string[] includes)
    {
        var filterDictionary = new Dictionary<string, object>();

        foreach (var filter in filters)
        {
            var parts = filter.Split('=');
            if (parts.Length == 2)
            {
                filterDictionary.Add(parts[0], parts[1]);
            }
        }
        
        var query = new GetFilteredExpensesQuery(filterDictionary, includes.ToList());
        var expenses = await _mediator.Send(query);
        
        return Ok(expenses);
    }
}