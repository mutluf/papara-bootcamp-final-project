using System.Linq.Expressions;
using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;

namespace DualPay.Application.Services;

public class ExpenseService :IExpenseService
{
    private readonly IGenericRepository<Expense> _expenseRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _expenseRepository = unitOfWork.GetRepository<Expense>();
    }


    public async Task<List<ExpenseDto>> GetAllAsync(params string[] includes)
    {
        List<Expense> datas = await _expenseRepository.GetAllAsync();
        return _mapper.Map<List<ExpenseDto>>(datas);
    }
    public async Task<List<ExpenseDto>> GetAllAsync(Expression<Func<Expense, bool>> predicate, params string[] includes)
    {
        List<Expense> datas = await _expenseRepository.GetAllAsync(predicate, includes);
        return _mapper.Map<List<ExpenseDto>>(datas);
    }

    public async Task<List<ExpenseDto>> Where(Expression<Func<Expense, bool>> predicate, params string[] includes)
    {
        List<Expense> datas = await _expenseRepository.Where(predicate,includes);
        return _mapper.Map<List<ExpenseDto>>(datas);
    }

    public async Task<ExpenseDto> GetByIdAsync(int id, params string[] includes)
    {
        Expense data = await _unitOfWork.GetRepository<Expense>().GetByIdAsync(id);
        return _mapper.Map<ExpenseDto>(data);
    }

    public async Task<ExpenseDto> AddAsync(ExpenseDto expenseDto)
    {
        Expense expense = _mapper.Map<Expense>(expenseDto);
        Expense data = await _expenseRepository.AddAsync(expense);
        await _expenseRepository.SaveChangesAsync();
        ExpenseDto dto = _mapper.Map<ExpenseDto>(data);
        return dto;
    }

    public async Task UpdateAsync(ExpenseDto expenseDto)
    {
        Expense expense = _mapper.Map<Expense>(expenseDto);
        _unitOfWork.GetRepository<Expense>().Update(expense);
        await _unitOfWork.Complete();
    }


    public async Task DeleteByIdAsync(int id)
    {
        await _expenseRepository.DeleteByIdAsync(id);
    }

    public async Task<List<ExpenseDto>> GetByFilterAsync(Dictionary<string, object> filters, params string[] includes)
    {
        List<Expense> datas =await _expenseRepository.GetByFilterAsync(filters, includes);
        return _mapper.Map<List<ExpenseDto>>(datas);
    }
}