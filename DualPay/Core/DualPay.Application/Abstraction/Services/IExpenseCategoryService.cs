using System.Linq.Expressions;
using DualPay.Application.Common.Models;
using DualPay.Application.Common.Models.Requests;
using DualPay.Application.Models.Responses;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;

public interface IExpenseCategoryService
{
    ApiResponse<List<ExpenseCategoryResponse>> GetAll(bool tracking);
    ApiResponse<List<ExpenseCategoryResponse>> GetWhere(Expression<Func<ExpenseCategory, bool>> method, bool tracking = true);
    Task<ApiResponse<ExpenseCategoryResponse>> GetByIdAsync(int id, bool tracking = false);
    Task<ApiResponse<ExpenseCategoryResponse>> AddAsync(CreateExpenseCategoryRequest request);
    ApiResponse<object> Update(UpdateExpenseCategoryRequest request);
    Task DeleteByIdAsync(int id);
}