using System.Linq.Expressions;
using DualPay.Application.Abstraction;
using DualPay.Domain.Entities.Common;
using DualPay.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DualPay.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DualPayDbContext _dbContext;

    public GenericRepository(DualPayDbContext context)
    {
        _dbContext = context;
    }
    public DbSet<TEntity> Table => _dbContext.Set<TEntity>();
    
    public IQueryable<TEntity> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
        {
            query = query.AsNoTracking();

        }
        return query;
    }
    public async Task<TEntity> GetByIdAsync(int id, bool tracking = true)
        
    {
        var query = Table.AsQueryable();
        if (!tracking)
        {
            query = Table.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(data => data.Id == id);
    }
    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method).AsQueryable();
        if (!tracking)
        {
            query = query.AsNoTracking();

        }
        return query;
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }
    
    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity != null)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}