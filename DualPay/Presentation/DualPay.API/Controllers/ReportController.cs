using DualPay.API.Attributes;
using DualPay.Application.DTOs.Reports;
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
    
    /// <summary>
    /// Retrieves payments within a specific date range - [ADMIN ONLY]
    /// </summary>
    [HttpGet("payment-summary")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<GetPaymentsReportQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentSummary([FromQuery] GetPaymentsReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves employee's all expenses within a specific date range - admin and personnel can see own data
    /// </summary>
    [HttpGet("employee-expenses/{EmployeeId}")]
    [AuthorizeEmployee]
    public async Task<IActionResult> GetEmployeeExpenses([FromRoute] GetEmployeeExpenseReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves employee's expenses with status "Paid" within a specific date range [ADMIN ONLY]
    /// </summary>
    [HttpGet("employee-spending-summary")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<GetEmployeeSpendingReportQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeSpendingSummary([FromQuery]GetEmployeeSpendingReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves category payments within a specific date range [ADMIN ONLY]
    /// </summary>
    [HttpGet("category-expense-report")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<GetCategoryExpenseReportQueryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryExpenseReport([FromQuery] GetCategoryExpenseReportQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
