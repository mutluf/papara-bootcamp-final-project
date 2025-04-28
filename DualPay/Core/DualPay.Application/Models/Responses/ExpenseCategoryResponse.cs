using DualPay.Application.Abstraction;
using DualPay.Domain.Entities;

namespace DualPay.Application.Models.Responses;

public class ExpenseCategoryResponse
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}