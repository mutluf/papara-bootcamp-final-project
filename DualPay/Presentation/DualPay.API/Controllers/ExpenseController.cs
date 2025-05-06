using System.Security.Claims;
using DualPay.API.Attributes;
using DualPay.Application.Common.Models;
using DualPay.Application.Features.Commands;
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

    /// <summary>
    /// Admin can see all expenses(except Pending Status), User can see own expenses 
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetAll()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        bool isAdmin = User.IsInRole("Admin");
        
        GetAllExpensesQueryRequest request = new GetAllExpensesQueryRequest();
        request.UserId =  isAdmin ? null : userId;
        ApiResponse<List<ExpenseResponse>> result = await _mediator.Send(request);
        return Ok(result);
    }
    
    /// <summary>
    /// [USER ONLY FOR OWN EXPENSE AND ADMIN]
    /// </summary>
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
    
    /// <summary>
    /// [USER ONLY]
    /// </summary>
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
    
    /// <summary>
    /// [USER ONLY FOR OWN EXPENSE]
    /// </summary>
    [HttpPut("{id}")]
    [UserExpenseAuthorization]
    public async Task<IActionResult> Update([FromBody] UpdateExpenseCommandRequest request,  [FromRoute] int id)
    {
        request.Id = id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    /// <summary>
    /// [USER ONLY FOR OWN EXPENSE]
    /// </summary>
    [HttpDelete("{id}")]
    [UserExpenseAuthorization]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteExpenseCommandRequest request = new DeleteExpenseCommandRequest();
        request.Id = id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    /// <summary>
    /// [USER ONLY FOR OWN EXPENSE] to send own expense for approval
    /// </summary>
    [HttpPost("submit/{id}")]
    [UserExpenseAuthorization]
    public async Task<IActionResult> SendExpenseToApproval([FromBody] SendExpenseToApprovalCommandRequest request, [FromRoute] int id)
    {
        request.ExpenseId = id;
        ApiResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    
    /// <summary>
    /// [ADMIN ONLY] Provide Date and UTC hour to start payment process (Background job working)
    /// </summary>
    [HttpPost("approve/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveExpenseAsync([FromBody] ApproveExpenseCommandRequest request, [FromRoute] int id)
    {
        request.ExpenseId = id;
        ApiResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpPost("reject/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectExpenseAsync([FromBody] RejectExpenseCommandRequest request, [FromRoute] int id)
    {
        request.ExpenseId = id;
        ApiResponse response = await _mediator.Send(request);
        return Ok(response);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
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