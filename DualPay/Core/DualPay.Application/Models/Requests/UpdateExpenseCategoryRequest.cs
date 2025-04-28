namespace DualPay.Application.Common.Models.Requests;

public class UpdateExpenseCategoryRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}