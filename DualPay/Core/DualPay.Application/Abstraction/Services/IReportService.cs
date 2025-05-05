using DualPay.Application.DTOs.Reports;

namespace DualPay.Application.Abstraction.Services;
public interface IReportService
{
    Task<List<EmployeeExpenseReportDto>> GetEmployeeExpenseHistoryReportAsync(int employeeId);
    Task<List<PaymentReportDto>> GetPaymentsReportAsync(DateTime startDate, DateTime endDate);
    Task<List<EmployeeSpendingReportDto>> GetEmployeeSpendingsReportAsync(DateTime startDate, DateTime endDate);
    Task<List<CategoryExpenseReportDto>> GetCategoryExpenseReportAsync(DateTime startDate, DateTime endDate);
}