using DualPay.API.Attributes;
using DualPay.Application.Common.Models;
using DualPay.Application.Features.Commands;
using DualPay.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeResponse = DualPay.Application.Features.Queries.EmployeeResponse;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        GetAllEmployeesQueryRequest request = new GetAllEmployeesQueryRequest();
        ApiResponse<List<EmployeeResponse>> result =await _mediator.Send(request);
        return Ok(result);
    }

    /// <summary>
    /// [USER ONLY FOR OWN DATA AND ADMIN]
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    [AuthorizeEmployeeForOwnExpense]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetEmployeeByIdRequest request = new GetEmployeeByIdRequest();
        request.Id = id;
        ApiResponse<EmployeeDetailResponse> result = await _mediator.Send(request);
        return Ok(result);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommandRequest request)
    {
        ApiResponse<EmployeeResponse> apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommandRequest request,  [FromRoute] int id)
    {
        request.Id= id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    /// <summary>
    /// [ADMIN ONLY]
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteEmployeeCommandRequest request = new DeleteEmployeeCommandRequest();
        request.Id = id;
        ApiResponse apiResponse =await _mediator.Send(request);
        return Ok(apiResponse);
    }
}