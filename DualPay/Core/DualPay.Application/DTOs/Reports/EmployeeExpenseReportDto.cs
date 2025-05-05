namespace DualPay.Application.DTOs.Reports;

public class EmployeeExpenseReportDto
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int ExpenseId { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string CreatedDate { get; set; }
}