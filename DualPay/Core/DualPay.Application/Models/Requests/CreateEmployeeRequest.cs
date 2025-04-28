namespace DualPay.Application.Common.Models.Requests;

public class CreateEmployeeRequest
{
    public string Name{ get; set; }
    public string Surname{ get; set; }
    public string Email{ get; set; }
    public string PhoneNumber{ get; set; }
    public string IdentityNumber {get; set;}
}