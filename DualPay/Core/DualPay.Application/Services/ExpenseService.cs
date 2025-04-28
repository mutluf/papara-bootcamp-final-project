using System.Linq.Expressions;
using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.Common.Models.Requests;
using DualPay.Application.Models.Responses;
using DualPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DualPay.Application.Services;

public class ExpenseService :IExpenseService
{
    private readonly IGenericRepository<Expense> _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _expenseRepository = unitOfWork.GetRepository<Expense>();
    }

    public ApiResponse<List<ExpenseResponse>> GetAll(bool tracking)
    {
        var datas = _expenseRepository.GetAll(tracking).ToList();
        var mapped = _mapper.Map<List<ExpenseResponse>>(datas);
        
        var apiResponse = new ApiResponse<List<ExpenseResponse>>(mapped); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public ApiResponse<List<ExpenseResponse>> GetWhere(Expression<Func<Expense, bool>> method, bool tracking = true)
    {
        var datas = _expenseRepository.GetWhere(method,tracking).ToList();
        var mapped = _mapper.Map<List<ExpenseResponse>>(datas);
        
        var apiResponse = new ApiResponse<List<ExpenseResponse>>(mapped); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public async Task<ApiResponse<ExpenseResponse>> GetByIdAsync(int id, bool tracking = true)
    {
        var data = await _expenseRepository.GetByIdAsync(id,tracking);
        var mapped = _mapper.Map<ExpenseResponse>(data);
        
        var apiResponse = new ApiResponse<ExpenseResponse>(mapped); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public async Task<ApiResponse<ExpenseResponse>> AddAsync(CreateExpenseRequest request)
    {
        var entity = _mapper.Map<Expense>(request);
        var data = await _expenseRepository.AddAsync(entity);
        
        var response = _mapper.Map<ExpenseResponse>(data);
        var apiResponse = new ApiResponse<ExpenseResponse>(response); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public ApiResponse<object> Update(UpdateExpenseRequest request)
    {
        var entity = _mapper.Map<Expense>(request);
        _expenseRepository.Update(entity);
        return new ApiResponse<object>(){Success=true};
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _expenseRepository.DeleteByIdAsync(id);
    }
}