using System.Linq.Expressions;
using DualPay.Application.Common.Models;
using DualPay.Application.Common.Models.Requests;
using DualPay.Application.Models.Responses;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;

public interface IExpenseService
{
    ApiResponse<List<ExpenseResponse>> GetAll(bool tracking);
    ApiResponse<List<ExpenseResponse>> GetWhere(Expression<Func<Expense, bool>> method, bool tracking = true);
    Task<ApiResponse<ExpenseResponse>> GetByIdAsync(int id, bool tracking = false);
    Task<ApiResponse<ExpenseResponse>> AddAsync(CreateExpenseRequest request);
    ApiResponse<object> Update(UpdateExpenseRequest request);
    Task DeleteByIdAsync(int id);
}