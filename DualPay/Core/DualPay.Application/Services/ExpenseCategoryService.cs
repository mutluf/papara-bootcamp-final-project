using System.Linq.Expressions;
using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.Application.Services;

public class ExpenseCategoryService :IExpenseCategoryService
{
    private readonly IGenericRepository<ExpenseCategory> _expenseCategoryRepository;
    private readonly IMapper _mapper;
    public ExpenseCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _expenseCategoryRepository = unitOfWork.GetRepository<ExpenseCategory>();
    }
    
    public async Task<List<ExpenseCategoryDto>> GetAllAsync(params string[] includes)
    {
        List<ExpenseCategory> datas = await _expenseCategoryRepository.GetAllAsync(includes);
        List<ExpenseCategoryDto> mapped = _mapper.Map<List<ExpenseCategoryDto>>(datas);
        return mapped;
    }
    public async Task<List<ExpenseCategoryDto>> GetAllAsync(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes)
    {
        List<ExpenseCategory> datas = await _expenseCategoryRepository.GetAllAsync(predicate, includes);
        List<ExpenseCategoryDto> mapped = _mapper.Map<List<ExpenseCategoryDto>>(datas);
        return mapped;
    }

    public async Task<List<ExpenseCategoryDto>> Where(Expression<Func<ExpenseCategory, bool>> predicate, params string[] includes)
    {
        List<ExpenseCategory> datas = await _expenseCategoryRepository.Where(predicate,includes);
        List<ExpenseCategoryDto> mapped = _mapper.Map<List<ExpenseCategoryDto>>(datas);

        return mapped;
    }

    public async Task<ExpenseCategoryDto> GetByIdAsync(int id, params string[] includes)
    {
        ExpenseCategory data = await _expenseCategoryRepository.GetByIdAsync(id);
        ExpenseCategoryDto mapped = _mapper.Map<ExpenseCategoryDto>(data);

        return mapped;
    }

    public async Task<ExpenseCategoryDto> AddAsync(ExpenseCategoryDto expenseCategoryDto)
    {
        ExpenseCategory entity = _mapper.Map<ExpenseCategory>(expenseCategoryDto);
        ExpenseCategory data = await _expenseCategoryRepository.AddAsync(entity);
        await _expenseCategoryRepository.SaveChangesAsync();
        
        ExpenseCategoryDto mapped = _mapper.Map<ExpenseCategoryDto>(data);
        return mapped;
    }

    public async Task UpdateAsync(ExpenseCategoryDto expenseCategoryDto)
    {
        ExpenseCategory entity = _mapper.Map<ExpenseCategory>(expenseCategoryDto);
        _expenseCategoryRepository.Update(entity);
        await _expenseCategoryRepository.SaveChangesAsync();
    }
    
    public async Task DeleteByIdAsync(int id)
    {
        await _expenseCategoryRepository.DeleteByIdAsync(id);
        await _expenseCategoryRepository.SaveChangesAsync();
    }
}