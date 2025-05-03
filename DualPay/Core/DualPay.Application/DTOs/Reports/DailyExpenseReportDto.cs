namespace DualPay.Application.DTOs.Reports;

public class DailyExpenseReportDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime ExpenseDate { get; set; }
    public decimal TotalAmount { get; set; }
}