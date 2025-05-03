using System.Linq.Expressions;
using DualPay.Domain.Entities;

namespace DualPay.Application.Abstraction.Services;

public interface IEmployeeService
{
    Task<Employee> GetByIdAsync(int id, params string[] includes);
    Task<List<Employee>> GetAllAsync(params string[] includes);
    Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>> predicate, params string[] includes);
    Task<List<Employee>> Where(Expression<Func<Employee, bool>> predicate, params string[] includes);
    Task<Employee> AddAsync(Employee entity);
    Task UpdateAsync(Employee entity);
    Task DeleteByIdAsync(int id);
}