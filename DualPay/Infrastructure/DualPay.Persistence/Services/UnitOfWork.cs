using DualPay.Application.Abstraction;
using DualPay.Domain.Entities.Common;
using DualPay.Persistence.Context;
using DualPay.Persistence.Repositories;

namespace DualPay.Persistence.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly DualPayDbContext _dbContext;
    
    public UnitOfWork(DualPayDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        return new GenericRepository<TEntity>(_dbContext);
    }
    
    public async Task Complete()
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Error occurred while saving changes to the database.");
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private bool _disposed = false;

}
