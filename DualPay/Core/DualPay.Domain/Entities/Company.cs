using DualPay.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class Company:BaseEntity
{
    public string Name { get; set; }
    public List<BankAccount>? BankAccounts { get; set; }
    public List<Employee>? Employees { get; set; }
}

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
  
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
                
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
                
        builder.HasMany(x => x.Employees)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .IsRequired(false)
            .IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.BankAccounts)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .IsRequired(false)
            .IsRequired(false).OnDelete(DeleteBehavior.Cascade);
       
    }
}  