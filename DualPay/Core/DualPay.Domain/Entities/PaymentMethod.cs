using DualPay.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class PaymentMethod :BaseEntity
{
    public string Name { get; set; }
    public string Provider { get; set; }
    public bool IsDefault { get; set; }
    public List<Payment>? Payments { get; set; }
}

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
  
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Provider).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IsDefault).IsRequired();
        
        builder.HasMany(x=>x.Payments)
            .WithOne(x=>x.PaymentMethod)
            .HasForeignKey(x=>x.PaymentMethodId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}  