using System.Linq.Expressions;
using DualPay.Application.Common.Models;
using DualPay.Application.Common.Models.Requests;
using DualPay.Application.Models.Responses;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;

public interface IEmployeeService
{
    
    ApiResponse<List<EmployeeResponse>> GetAll(bool tracking);
    ApiResponse<List<EmployeeResponse>> GetWhere(Expression<Func<Employee, bool>> method, bool tracking = true);
    Task<ApiResponse<EmployeeResponse>> GetByIdAsync(int id, bool tracking = false);
    Task<ApiResponse<EmployeeResponse>> AddAsync(CreateEmployeeRequest request);
    ApiResponse<object> Update(UpdateEmployeeRequest request);
    Task DeleteByIdAsync(int id);
}