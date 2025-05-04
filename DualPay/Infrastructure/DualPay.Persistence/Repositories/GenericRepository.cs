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
   
    public async Task DeleteByIdAsync(int id)
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

    public async Task<TEntity> GetByIdAsync(int id, params string[] includes)
    {
        var query = Table.AsQueryable();
        query = includes.Aggregate(query, (current, inc) => EntityFrameworkQueryableExtensions.Include(current, inc)).AsNoTracking();
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
    
    public async Task<List<TEntity>> GetByFilterAsync(Dictionary<string, object> filters, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (filters != null && filters.Any())
        {
            foreach (var filter in filters)
            {
                query = ApplyFilter(query, filter.Key, filter.Value);
            }
        }
        
        if (includes.Any())
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }
    private IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, string fieldName, object value)
    {
        var property = typeof(TEntity).GetProperty(fieldName);
        if (property != null)
        {
            var propertyType = property.PropertyType;
            if (fieldName.Equals("Status", StringComparison.OrdinalIgnoreCase) && propertyType == typeof(string))
            {
                var enumStringValue = value.ToString();
                query = query.Where(x => EF.Property<string>(x, fieldName) == enumStringValue);
            }
            else if (propertyType == typeof(string))
            {
                query = query.Where(x => EF.Functions.Like(EF.Property<string>(x, fieldName), $"%{value}%"));
            }
            else if (propertyType.IsEnum)
            {
                var enumType = property.PropertyType;
                if (Enum.IsDefined(enumType, value))
                {
                    query = query.Where(x => EF.Property<int>(x, fieldName) == (int)Enum.Parse(enumType, value.ToString()));
                }
            }
            else if (propertyType == typeof(int) || propertyType == typeof(double) || propertyType == typeof(decimal))
            {
                query = query.Where(x => EF.Property<object>(x, fieldName).Equals(value));
            }
            else
            {
                query = query.Where(x => EF.Property<object>(x, fieldName).Equals(value));
            }
        }
        return query;
    }
}