using DualPay.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class Employee:AppUser
{
    public string PhoneNumber{ get; set; }
    public virtual List<Demand>? Demands { get; set; }
    public virtual List<BankAccount>? BankAccounts { get; set; }
    public int? CompanyId {get; set;}
    public virtual Company? Company {get; set;}
}

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.PhoneNumber).IsRequired();

        builder.HasMany(x => x.Demands)
            .WithOne(x=>x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.BankAccounts)
            .WithOne(x=>x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Company)
            .WithMany(x=>x.Employees)
            .HasForeignKey(x => x.CompanyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
