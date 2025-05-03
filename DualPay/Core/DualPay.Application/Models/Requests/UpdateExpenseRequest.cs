using DualPay.Domain.Entities;

namespace DualPay.Application.Common.Models.Requests;

public class UpdateExpenseRequest
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string? DocumentUrl { get; set; }
    public string Location { get; set; }

}