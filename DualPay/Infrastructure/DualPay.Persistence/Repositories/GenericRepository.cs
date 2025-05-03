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
    
   public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }
   
    public async Task DeleteByIdAsync(long id)
    {
        var entity = await Table.FindAsync(id);
        if (entity != null)
        {
            Table.Remove(entity);
        }
    }
    
    public void Delete(TEntity entity)
    {
        Table.Remove(entity);
    }
    
    public async Task<List<TEntity>> GetAllAsync(params string[] includes)
    {
        var query = Table.AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.ToListAsync(query);
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        var query = Table.Where(predicate).AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.ToListAsync(query);
    }

    public async Task<TEntity> GetByIdAsync(long id, params string[] includes)
    {
        var query = Table.AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query, x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        Table.Update(entity);
    }

    public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        var query = Table.Where(predicate).AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc));
        return await EntityFrameworkQueryableExtensions.ToListAsync(query);
    }
}