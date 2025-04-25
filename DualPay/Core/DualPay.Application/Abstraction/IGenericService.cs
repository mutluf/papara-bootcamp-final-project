using System.Linq.Expressions;
using DualPay.Domain.Entities.Common;

namespace DualPay.Application.Abstraction;

public interface IGenericService<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    IEnumerable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}