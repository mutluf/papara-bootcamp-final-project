using System.Linq.Expressions;
using DualPay.Domain.Entities.Common;

namespace DualPay.Application.Abstraction;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll(bool tracking=true);
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true);
    Task<TEntity> GetByIdAsync(int id, bool tracking = true);
    Task<TEntity> AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task DeleteByIdAsync(int id);
    Task SaveChangesAsync();
}