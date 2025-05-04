namespace DualPay.Application.Abstraction;

public interface IEventPublishService
{
    Task PublishAsync<T>(T @event) where T : IApplicationEvent;
}