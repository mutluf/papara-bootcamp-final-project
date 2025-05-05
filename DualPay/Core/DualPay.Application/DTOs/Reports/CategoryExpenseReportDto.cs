namespace DualPay.Application.DTOs.Reports;

public class CategoryExpenseReportDto
{
    public string Category { get; set; }
    public string AverageSpending { get; set; }
    public int TotalAmount { get; set; }
    public int TotalTransactions { get; set; }
}