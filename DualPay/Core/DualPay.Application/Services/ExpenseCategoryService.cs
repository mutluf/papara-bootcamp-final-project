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

public class ExpenseCategoryService :IExpenseCategoryService
{
    private readonly IGenericRepository<ExpenseCategory> _expenseCategoryRepository;
    private readonly IMapper _mapper;

    public ExpenseCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _expenseCategoryRepository = unitOfWork.GetRepository<ExpenseCategory>();
    }

    public ApiResponse<List<ExpenseCategoryResponse>> GetAll(bool tracking)
    {
        var datas = _expenseCategoryRepository.GetAll(tracking).ToList();
        var mapped = _mapper.Map<List<ExpenseCategoryResponse>>(datas);
        
        var apiResponse = new ApiResponse<List<ExpenseCategoryResponse>>(mapped); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public ApiResponse<List<ExpenseCategoryResponse>> GetWhere(Expression<Func<ExpenseCategory, bool>> method, bool tracking = true)
    {
        var datas = _expenseCategoryRepository.GetWhere(method,tracking).ToList();
        var mapped = _mapper.Map<List<ExpenseCategoryResponse>>(datas);
        
        var apiResponse = new ApiResponse<List<ExpenseCategoryResponse>>(mapped); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> GetByIdAsync(int id, bool tracking = true)
    {
        var data = await _expenseCategoryRepository.GetByIdAsync(id,tracking);
        var mapped = _mapper.Map<ExpenseCategoryResponse>(data);
        
        var apiResponse = new ApiResponse<ExpenseCategoryResponse>(mapped); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> AddAsync(CreateExpenseCategoryRequest request)
    {
        var entity = _mapper.Map<ExpenseCategory>(request);
        var data = await _expenseCategoryRepository.AddAsync(entity);
        
        var response = _mapper.Map<ExpenseCategoryResponse>(data);
        var apiResponse = new ApiResponse<ExpenseCategoryResponse>(response); 
        apiResponse.Success = true;
        return apiResponse;
    }

    public ApiResponse<object> Update(UpdateExpenseCategoryRequest request)
    {
        var entity = _mapper.Map<ExpenseCategory>(request);
        _expenseCategoryRepository.Update(entity);
        return new ApiResponse<object>(){Success=true};
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _expenseCategoryRepository.DeleteByIdAsync(id);
    }
}