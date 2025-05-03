using DualPay.Application.DTOs.Reports;

namespace DualPay.Application.Abstraction.Services;

public interface IReportService
{
    Task<List<DailyExpenseReportDto>> GetDailyReportAsync();
}