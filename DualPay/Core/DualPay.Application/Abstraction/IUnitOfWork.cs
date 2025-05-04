using DualPay.Domain.Entities.Common;

namespace DualPay.Application.Abstraction;
public interface IUnitOfWork : IDisposable
{
    Task Complete();
    IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
}