namespace DualPay.Application.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string AccountNumber { get; set; }
    public string IdentityNumber { get; set; }
}