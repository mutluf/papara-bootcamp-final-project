using System.Linq.Expressions;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;

public interface IExpenseService
{
    Task<Expense> GetByIdAsync(int id, params string[] includes);
    Task<List<Expense>> GetAllAsync(params string[] includes);
    Task<List<Expense>> GetAllAsync(Expression<Func<Expense, bool>> predicate, params string[] includes);
    Task<List<Expense>> Where(Expression<Func<Expense, bool>> predicate, params string[] includes);
    Task<Expense> AddAsync(Expense entity);
    Task UpdateAsync(Expense entity);
    Task DeleteByIdAsync(int id);
}