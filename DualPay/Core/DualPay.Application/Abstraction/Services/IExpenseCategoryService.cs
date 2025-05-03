using System.Linq.Expressions;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;

public interface IExpenseCategoryService
{
    Task<ExpenseCategoryDto> GetByIdAsync(int id, params string[] includes);
    Task<List<ExpenseCategoryDto>> GetAllAsync(params string[] includes);
    Task<List<ExpenseCategoryDto>> GetAllAsync(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes);
    Task<List<ExpenseCategoryDto>> Where(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes);
    Task<ExpenseCategoryDto> AddAsync(ExpenseCategoryDto entity);
    Task UpdateAsync(ExpenseCategoryDto entity);
    Task DeleteByIdAsync(int id);
}