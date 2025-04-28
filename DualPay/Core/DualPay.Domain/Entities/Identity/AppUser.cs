using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities.Identity;

public class AppUser : IdentityUser<int>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    
    public Employee? Employee { get; set; }
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
        
        builder.Ignore(x => x.Employee);
    }
}  