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
    
    public async Task<List<PaymentReportDto>> GetPaymentsReportAsync(DateTime startDate, DateTime endDate)
    {
        return await _reportRepository.GetPaymentsReportAsync(startDate, endDate);
    }

    public async Task<List<EmployeeSpendingReportDto>> GetEmployeeSpendingsReportAsync(DateTime startDate, DateTime endDate)
    {
        return await _reportRepository.GetEmployeeSpendingsReportAsync(startDate, endDate);
    }

    public async Task<List<CategoryExpenseReportDto>> GetCategoryExpenseReportAsync(DateTime startDate, DateTime endDate)
    {
        return await _reportRepository.GetCategoryExpenseReportAsync(startDate, endDate);
    }

    public async Task<List<EmployeeExpenseReportDto>> GetEmployeeExpenseHistoryReportAsync(int employeeId)
    {
        return await _reportRepository.GetEmployeeExpenseHistoryReportAsync(employeeId);
    }
}