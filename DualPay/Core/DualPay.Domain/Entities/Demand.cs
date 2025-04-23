using DualPay.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class Demand : BaseEntity
{
    public int ExpenseId { get; set; }
    public virtual Expense Expense { get; set; }

    public DemandStatus DemandStatus { get; set; } = DemandStatus.Pending;
    public DateTime? ApprovedDate { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string? RejectionReason { get; set; }

    public bool? IsRejected { get; set; }
    public bool? IsPaid { get; set; }
    public bool? IsApproved { get; set; }

    public int? ApprovedBy { get; set; }
    public virtual Admin? ApprovedByUser { get; set; }

    public int? RejectedBy { get; set; }
    public virtual Admin? RejectedByUser { get; set; }

    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual Payment? Payment { get; set; }
}

public class DemandConfiguration : IEntityTypeConfiguration<Demand>
{
    public void Configure(EntityTypeBuilder<Demand> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.Property(x => x.DemandStatus).IsRequired();
        builder.Property(x => x.RejectionReason).HasMaxLength(100);
        
        builder.HasOne(x => x.ApprovedByUser)
            .WithMany()
            .HasForeignKey(x => x.ApprovedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.RejectedByUser)
            .WithMany()
            .HasForeignKey(x => x.RejectedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Expense)
            .WithOne(x => x.Demand)
            .HasForeignKey<Demand>(x => x.ExpenseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Payment)
            .WithOne()
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
