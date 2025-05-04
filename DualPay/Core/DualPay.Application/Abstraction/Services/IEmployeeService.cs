using System.Linq.Expressions;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;
public interface IEmployeeService
{
    Task<EmployeeDto> GetByIdAsync(int id, params string[] includes);
    Task<List<EmployeeDto>> GetAllAsync(params string[] includes);
    Task<List<EmployeeDto>> GetAllAsync(Expression<Func<Employee, bool>> predicate, params string[] includes);
    Task<List<EmployeeDto>> Where(Expression<Func<Employee, bool>> predicate, params string[] includes);
    Task<EmployeeDto> AddAsync(EmployeeDto entity);
    Task UpdateAsync(EmployeeDto entity);
    Task DeleteByIdAsync(int id);
}