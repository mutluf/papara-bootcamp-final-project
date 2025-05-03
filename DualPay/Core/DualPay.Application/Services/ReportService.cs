using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.DTOs.Reports;

namespace DualPay.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<DailyExpenseReportDto>> GetDailyReportAsync()
    {
        return await _reportRepository.GetDailyExpenseReportAsync();
    }
}