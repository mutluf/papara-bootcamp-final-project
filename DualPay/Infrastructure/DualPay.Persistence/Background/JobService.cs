using DualPay.Application.Abstraction;
using DualPay.Application.Events;
using Hangfire;

namespace DualPay.Persistence.Background;

public class JobService : IJobService
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private IEventPublishService _eventPublishService;
    
    public JobService(IBackgroundJobClient backgroundJobClient, IEventPublishService eventPublishService)
    {
        _backgroundJobClient = backgroundJobClient;
        _eventPublishService = eventPublishService;
    }

    public async Task ScheduleSendExpenseToPaymentAsync(ExpenseApprovedEvent expenseApprovedEvent, DateTime paymentDate)
    {
        var jobId = _backgroundJobClient.Schedule(
            () => PublishExpenseApprovedEvent(expenseApprovedEvent),
            paymentDate);
        var a = "";
    }
    
    public async Task PublishExpenseApprovedEvent(ExpenseApprovedEvent expenseApprovedEvent)
    {
        await _eventPublishService.PublishAsync(expenseApprovedEvent);
    }
}
