using DualPay.Application.Common.Models;
using DualPay.Application.Common.Models.Requests;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpenseController : ControllerBase
{
    // Employee expense girişi yapacak. İlk etapta data olacak.
    // EmployeeId setlenecek.
    // PUT yapısında, yani expense güncelleme yapısında auth dikkate alınacak.!
    
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(GetAllEmployeesQueryRequest request)
    {
        ApiResponse<List<EmployeeResponse>> result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(GetExpenseByIdRequest  request)
    {
        ApiResponse<ExpenseDetailResponse> result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
    
    [HttpPut("{id}")] // ektra auth olanla güncellemek istenen id arasında kontrol.
    public async Task<IActionResult> Update(UpdateExpenseRequest request)
    {
        // System.Security.Claims.ClaimTypes.NameIdentifier;
        // HttpContext.User.Claims
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