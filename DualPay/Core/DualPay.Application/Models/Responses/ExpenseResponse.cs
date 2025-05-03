using DualPay.Domain.Entities;

namespace DualPay.Application.Models.Responses;

public class ExpenseResponse
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? DocumentUrl { get; set; }
    public string Location { get; set; }
    public string CreatedByName { get; set; }
}