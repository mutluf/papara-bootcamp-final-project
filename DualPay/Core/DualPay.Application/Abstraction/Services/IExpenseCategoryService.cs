using System.Linq.Expressions;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;

public interface IExpenseCategoryService
{
    Task<ExpenseCategory> GetByIdAsync(int id, params string[] includes);
    Task<List<ExpenseCategory>> GetAllAsync(params string[] includes);
    Task<List<ExpenseCategory>> GetAllAsync(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes);
    Task<List<ExpenseCategory>> Where(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes);
    Task<ExpenseCategory> AddAsync(ExpenseCategory entity);
    Task UpdateAsync(ExpenseCategory entity);
    Task DeleteByIdAsync(int id);
}