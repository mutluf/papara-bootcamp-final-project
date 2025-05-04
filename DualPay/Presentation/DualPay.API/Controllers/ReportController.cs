using DualPay.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Controllers;
[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    
    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }
    
    [HttpGet("daily-expense")]
    public async Task<IActionResult> GetDailyReport()
    {
        var result = await _reportService.GetDailyReportAsync();
        return Ok(result);
    }
}
