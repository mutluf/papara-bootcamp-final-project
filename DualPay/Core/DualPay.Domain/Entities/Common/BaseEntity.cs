namespace DualPay.Domain.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}