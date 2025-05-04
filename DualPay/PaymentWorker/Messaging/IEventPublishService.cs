namespace PaymentWorker.Messaging;

public interface IEventPublishService
{
    Task PublishAsync<T>(T @event) where T : IApplicationEvent;
}

public interface IApplicationEvent
{
}