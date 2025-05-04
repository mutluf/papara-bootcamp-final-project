using DualPay.Application.DTOs.Reports;

namespace DualPay.Application.Abstraction;
public interface IReportRepository
{
    Task<List<DailyExpenseReportDto>> GetDailyExpenseReportAsync();
}