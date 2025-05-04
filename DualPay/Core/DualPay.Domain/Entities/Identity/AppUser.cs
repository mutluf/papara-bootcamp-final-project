using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities.Identity;
public class AppUser : IdentityUser<int>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Employee? Employee { get; set; }
    
    //both field in the below are not appropriate for this entity but I want to show you the cash flow
    public string? AccountNumber { get; set; }
    public Decimal? Balance { get; set; }
}

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired(false)
            .HasMaxLength(20);
        builder.Property(x => x.Surname)
            .IsRequired(false)
            .HasMaxLength(20);
        builder.Property(x => x.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.AccountNumber)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Ignore(x => x.Employee);
    }
}  