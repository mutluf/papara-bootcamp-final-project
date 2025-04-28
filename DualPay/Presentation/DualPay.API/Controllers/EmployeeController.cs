using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = _employeeService.GetAll(false);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _employeeService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeRequest request)
    {
        var result = await _employeeService.AddAsync(request);
        return Ok(result);
    }
    
    [HttpPut]
    public IActionResult Create(UpdateEmployeeRequest request)
    {
        var result = _employeeService.Update(request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Create([FromRoute] int id)
    {
        await _employeeService.DeleteByIdAsync(id);
        return Ok();
    }
}