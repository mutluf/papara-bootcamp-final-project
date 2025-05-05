using DualPay.API.Attributes;
using DualPay.Application.Features.Queries.Report;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;
[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("payment-summary")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetPaymentSummary([FromQuery] GetPaymentsReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("employee-expenses/{EmployeeId}")]
    [AuthorizeEmployee]
    public async Task<IActionResult> GetEmployeeExpenses([FromRoute] GetEmployeeExpenseReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("employee-spending-summary")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetEmployeeSpendingSummary([FromQuery]GetEmployeeSpendingReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    
    [HttpGet("category-expense-report")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCategoryExpenseReport([FromQuery] GetCategoryExpenseReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
