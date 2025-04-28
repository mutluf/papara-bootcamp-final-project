namespace DualPay.Application.Common.Models.Requests;

public class UpdateEmployeeRequest
{
    public int Id{ get; set; }
    public string PhoneNumber{ get; set; }
    public string IdentityNumber {get; set;}
}