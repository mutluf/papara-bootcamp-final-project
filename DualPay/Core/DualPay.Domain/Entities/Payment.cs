using DualPay.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class Payment :BaseEntity
{
    public string Name { get; set; }
    public int DemandId { get; set; }
    public virtual Demand Demand { get; set; }
    public string Provider { get; set; }
    public bool? IsDefault { get; set; }
    public int PaymentMethodId { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public int FromAccount { get; set; }
    public int ToAccount { get; set; }
    public string ReferenceNumber { get; set; }
    
    public Expense Expense { get; set; }
    
    public int ExpenseId { get; set; }

}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedBy).IsRequired(); builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.Property(x => x.FromAccount).IsRequired();
        builder.Property(x => x.ToAccount).IsRequired();
        builder.Property(x => x.Provider).IsRequired().HasMaxLength(20);
        builder.Property(x => x.IsDefault).IsRequired(false);
        builder.Property(x => x.Status)
            .HasDefaultValue(PaymentStatus.Pending);
        builder.Property(x => x.ReferenceNumber).IsRequired().HasMaxLength(50);
        
        builder.HasOne(x=>x.Demand)
            .WithOne(x=>x.Payment)
            .HasForeignKey<Payment>(x => x.DemandId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.PaymentMethod)
            .WithMany(x=>x.Payments)
            .HasForeignKey(x => x.PaymentMethodId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x=>x.Expense)
            .WithOne()
            .HasForeignKey<Payment>(x => x.ExpenseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        
    }
}