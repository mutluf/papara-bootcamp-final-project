using System.Linq.Expressions;
using DualPay.Application.Abstraction;
using DualPay.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DualPay.Application.Services;

public class GenericService<T> : IGenericService<T> where T : BaseEntity
{
    private readonly IUnitOfWork _unitOfWork;

    public GenericService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<T>> GetAllAsync()
    {
        var entities = _unitOfWork.GetRepository<T>().GetAll();
        return await entities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _unitOfWork.GetRepository<T>().GetByIdAsync(id);
    }
    

    public IEnumerable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        return _unitOfWork.GetRepository<T>().GetWhere(method, tracking);
    }


    public async Task<T> AddAsync(T entity)
    {
        await _unitOfWork.GetRepository<T>().AddAsync(entity);
        await _unitOfWork.Complete();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _unitOfWork.GetRepository<T>().Update(entity);
        await _unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        T entity = await _unitOfWork.GetRepository<T>().GetByIdAsync(id);
        if (entity != null)
        {
            await _unitOfWork.GetRepository<T>().DeleteByIdAsync(id);
            await _unitOfWork.Complete();
        }
    }
}
