using DualPay.Domain.Entities;

namespace DualPay.Application.Models.Responses;

public class EmployeeResponse
{
    public string PhoneNumber{ get; set; }
    public virtual List<Demand>? Demands { get; set; }
    public virtual List<BankAccount>? BankAccounts { get; set; }
    public string IdentityNumber {get; set;}
}