using DualPay.Domain.Entities.Common;
using DualPay.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;
public class Employee : BaseEntity
{
    public int UserId { get; set; }
    public AppUser AppUser { get; set; }
    public string PhoneNumber { get; set; }
    public string AccountNumber { get; set; }
    public string IdentityNumber { get; set; }
    public virtual List<Expense>? Expenses { get; set; }
}

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(10);
        builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(11);
        builder.Property(x => x.AccountNumber).IsRequired().HasMaxLength(30);

        
        builder.HasOne(e => e.AppUser)
            .WithOne()
            .HasForeignKey<Employee>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x=> x.Expenses)
            .WithOne(x => x.Employee)
            .HasForeignKey(x=> x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}