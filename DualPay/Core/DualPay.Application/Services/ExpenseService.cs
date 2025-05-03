using System.Linq.Expressions;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Domain.Entities;

namespace DualPay.Application.Services;

public class ExpenseService :IExpenseService
{
    private readonly IGenericRepository<Expense> _expenseRepository;

    public ExpenseService(IUnitOfWork unitOfWork)
    {
        _expenseRepository = unitOfWork.GetRepository<Expense>();
    }


    public async Task<List<Expense>> GetAllAsync(params string[] includes)
    {
        var datas = await _expenseRepository.GetAllAsync(includes);
        return datas;
    }
    public async Task<List<Expense>> GetAllAsync(Expression<Func<Expense, bool>> predicate, params string[] includes)
    {
        var datas = await _expenseRepository.GetAllAsync(predicate, includes);
        return datas;
    }

    public async Task<List<Expense>> Where(Expression<Func<Expense, bool>> predicate, params string[] includes)
    {
        var datas = await _expenseRepository.Where(predicate,includes);
        return datas;
    }

    public async Task<Expense> GetByIdAsync(int id, params string[] includes)
    {
        var data = await _expenseRepository.GetByIdAsync(id);
        return data;
    }

    public async Task<Expense> AddAsync(Expense entity)
    {
        return await _expenseRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Expense entity)
    {
        await _expenseRepository.GetByIdAsync(entity.Id);
    }


    public async Task DeleteByIdAsync(int id)
    {
        await _expenseRepository.DeleteByIdAsync(id);
    }
}