using DualPay.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class BankAccount :BaseEntity
{
        public int AccountNumber {get; set;}
        public string IBAN {get; set;}
        public string CurrencyCode {get; set;}
        public decimal Balance {get; set;}
        public bool? IsDefault {get; set;}
        public int? EmployeeId {get; set;}
        public virtual Employee? Employee {get; set;}
}

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
  
                builder.Property(x => x.CreatedBy).IsRequired();
                builder.Property(x => x.CreatedAt).IsRequired();
                builder.Property(x => x.UpdatedAt).IsRequired();
                
                builder.Property(x => x.AccountNumber).IsRequired();
                builder.Property(x => x.IBAN).IsRequired().HasMaxLength(26);
                builder.Property(x => x.Balance).IsRequired().HasPrecision(18,2);
                builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
                builder.Property(x => x.IsDefault).IsRequired(false);
                
                builder.HasOne(x => x.Employee)
                        .WithMany(x => x.BankAccounts)
                        .HasForeignKey(x => x.EmployeeId)
                        .IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
}  