using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DualPay.Persistence.Context;

public class DualPayDbContext :  IdentityDbContext<AppUser,AppRole,int>
{
    public DualPayDbContext(DbContextOptions<DualPayDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Demand> Demands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Expense).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
