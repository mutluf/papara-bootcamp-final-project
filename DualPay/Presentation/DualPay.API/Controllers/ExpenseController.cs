using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/expenses")]
public class ExpenseController : ControllerBase
{
    // Employee expense girişi yapacak. İlk etapta data olacak.
    // EmployeeId setlenecek.
    // PUT yapısında, yani expense güncelleme yapısında auth dikkate alınacak.!
    
    private readonly IExpenseService _ExpenseService;
    public ExpenseController(IExpenseService ExpenseService)
    {
        _ExpenseService = ExpenseService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = _ExpenseService.GetAll(false);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _ExpenseService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseRequest request)
    {
        var result = await _ExpenseService.AddAsync(request);
        return Ok(result);
    }
    
    [HttpPut("{id}")] // ektra auth olanla güncellemek istenen id arasında kontrol.
    public IActionResult Update(UpdateExpenseRequest request)
    {
        // System.Security.Claims.ClaimTypes.NameIdentifier;
        // HttpContext.User.Claims
        var result = _ExpenseService.Update(request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _ExpenseService.DeleteByIdAsync(id);
        return Ok();
    }
}