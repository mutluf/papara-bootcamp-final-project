using DualPay.Domain.Entities;

namespace DualPay.Application.DTOs;

public class ExpenseDto
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? DocumentUrl { get; set; }
    public string Location { get; set; }
    public int ? EmployeeId { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string? ApprovedBy { get; set; }
    public string? RejectedBy { get; set; }
    public string? RejectionReason { get; set; }
}