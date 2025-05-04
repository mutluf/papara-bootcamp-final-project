using System.Linq.Expressions;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;
public interface IExpenseService
{
    Task<ExpenseDto> GetByIdAsync(int id, params string[] includes);
    Task<List<ExpenseDto>> GetAllAsync(params string[] includes);
    Task<List<ExpenseDto>> GetAllAsync(Expression<Func<Expense, bool>> predicate, params string[] includes);
    Task<List<ExpenseDto>> Where(Expression<Func<Expense, bool>> predicate, params string[] includes);
    Task<ExpenseDto> AddAsync(ExpenseDto expenseDto);
    Task UpdateAsync(ExpenseDto expenseDto);
    Task DeleteByIdAsync(int id);
    Task<List<ExpenseDto>> GetByFilterAsync(Dictionary<string, object> filters, params string[] includes);
}