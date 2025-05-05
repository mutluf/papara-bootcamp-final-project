using DualPay.Application.DTOs.Reports;

namespace DualPay.Application.Abstraction;
public interface IReportRepository
{
    Task<List<EmployeeExpenseReportDto>> GetEmployeeExpenseHistoryReportAsync(int employeeId);
    Task<List<PaymentReportDto>> GetPaymentsReportAsync(DateTime startDate, DateTime endDate);
    Task<List<EmployeeSpendingReportDto>> GetEmployeeSpendingsReportAsync(DateTime startDate, DateTime endDate);
    Task<List<CategoryExpenseReportDto>> GetCategoryExpenseReportAsync(DateTime startDate, DateTime endDate);
}