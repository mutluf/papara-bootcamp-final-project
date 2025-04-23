using System.ComponentModel.DataAnnotations.Schema;
using DualPay.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DualPay.Domain.Entities;

public class ExpenseCategory: BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<Expense>? Expenses { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
  
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(100);
        
        builder.HasMany(x=> x.Expenses)
            .WithOne(x=> x.ExpenseCategory)
            .IsRequired()
            .HasForeignKey(x=>x.ExpenseCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}   