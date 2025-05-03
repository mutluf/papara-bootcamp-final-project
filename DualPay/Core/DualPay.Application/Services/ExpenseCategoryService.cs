using System.Linq.Expressions;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Domain.Entities;

namespace DualPay.Application.Services;

public class ExpenseCategoryService :IExpenseCategoryService
{
    private readonly IGenericRepository<ExpenseCategory> _expenseCategoryRepository;

    public ExpenseCategoryService(IUnitOfWork unitOfWork)
    {
        _expenseCategoryRepository = unitOfWork.GetRepository<ExpenseCategory>();
    }


    public async Task<List<ExpenseCategory>> GetAllAsync(params string[] includes)
    {
        var datas = await _expenseCategoryRepository.GetAllAsync(includes);
        return datas;
    }
    public async Task<List<ExpenseCategory>> GetAllAsync(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes)
    {
        var datas = await _expenseCategoryRepository.GetAllAsync(predicate, includes);
        return datas;
    }

    public async Task<List<ExpenseCategory>> Where(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes)
    {
        var datas = await _expenseCategoryRepository.Where(predicate,includes);
        return datas;
    }

    public async Task<ExpenseCategory> GetByIdAsync(int id, params string[] includes)
    {
        var data = await _expenseCategoryRepository.GetByIdAsync(id);
        return data;
    }

    public async Task<ExpenseCategory> AddAsync(ExpenseCategory entity)
    {
        return await _expenseCategoryRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(ExpenseCategory entity)
    {
        await _expenseCategoryRepository.GetByIdAsync(entity.Id);
    }


    public async Task DeleteByIdAsync(int id)
    {
        await _expenseCategoryRepository.DeleteByIdAsync(id);
    }
}