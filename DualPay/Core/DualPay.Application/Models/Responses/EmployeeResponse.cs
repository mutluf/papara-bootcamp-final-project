using DualPay.Domain.Entities;

namespace DualPay.Application.Models.Responses;

public class EmployeeResponse
{
    public int Id { get; set; }
    public string PhoneNumber{ get; set; }
    public List<Expense>? Expenses { get; set; }
    public string AccountNumber { get; set; }
    public string IdentityNumber {get; set;}
}