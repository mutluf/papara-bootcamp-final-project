using DualPay.Application.Common.Models;
using DualPay.Application.Features.Commands;
using DualPay.Application.Features.Queries;
using MediatR;
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

    [HttpGet]
    public async Task<IActionResult> GetAll(GetAllEmployeesQueryRequest request)
    {
        ApiResponse<List<EmployeeResponse>> result =await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] GetEmployeeByIdRequest request, [FromRoute] int id)
    {
        request.Id = id;
        ApiResponse<EmployeeDetailResponse> result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommandRequest request)
    {
        ApiResponse<Application.Features.Commands.EmployeeResponse> apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommandRequest request,  [FromRoute] int id)
    {
        request.Id= id;
        ApiResponse apiResponse = await _mediator.Send(request);
        return Ok(apiResponse);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteEmployeeCommandRequest request, [FromRoute] int id)
    {
        request.Id = id;
        ApiResponse apiResponse =await _mediator.Send(request);
        return Ok(apiResponse);
    }
}