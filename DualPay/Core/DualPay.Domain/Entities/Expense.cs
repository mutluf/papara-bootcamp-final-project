using System.ComponentModel.DataAnnotations.Schema;
using DualPay.Domain.Entities.Common;
using DualPay.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class Expense : BaseEntity
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public virtual ExpenseCategory ExpenseCategory { get; set; }
    public decimal Amount { get; set; }
    public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
    public string? DocumentUrl { get; set; }
    public string Location { get; set; }

    public virtual Payment? Payment { get; set; }
    
    public AppUser? AppUser { get; set; }
    public Employee? Employee { get; set; }
    
    public int ? EmployeeId { get; set; }
    
    public DateTime? ApprovedDate { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string? ApprovedBy { get; set; }
    public string? RejectedBy { get; set; }
    public string? RejectionReason { get; set; }
}

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.ApprovedDate);
        builder.Property(x => x.ApprovedBy);
        builder.Property(x => x.RejectedDate);
        builder.Property(x => x.RejectedBy);
        builder.Property(x => x.RejectionReason);

        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Location).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Status).IsRequired().HasDefaultValue(ExpenseStatus.Pending);
        builder.Property(x => x.DocumentUrl).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.RejectionReason).HasMaxLength(100);
        builder.Property(x => x.Status)
            .HasDefaultValue(ExpenseStatus.Pending);

        builder.HasOne(x => x.ExpenseCategory)
            .WithMany(x => x.Expenses)
            .HasForeignKey(x => x.ExpenseCategoryId)
            .IsRequired().OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Employee)
            .WithMany(x => x.Expenses)
            .IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.AppUser)
            .WithMany(
                )
            .IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        
        builder.Ignore(x=> x.Payment);
        builder.Ignore(x=> x.AppUser);
    }
}