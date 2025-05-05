using DualPay.Application.Events;

namespace DualPay.Application.Abstraction;

public interface IJobService
{
    Task ScheduleSendExpenseToPaymentAsync(ExpenseApprovedEvent expenseApprovedEvent, DateTime paymentDate);
}