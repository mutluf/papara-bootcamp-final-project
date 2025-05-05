namespace DualPay.Application.DTOs.Reports;

public class EmployeeSpendingReportDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalTransactions{ get; set; }
    public decimal AverageSpending{ get; set; }
}