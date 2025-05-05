using DualPay.Application.Abstraction;

namespace DualPay.Application.Events;

public class ExpenseApprovedEvent : IApplicationEvent
{
    public decimal Amount { get; set; }
    public string ToAccount { get; set; }
    public string FromAccount { get; set; }
    public int ExpenseId { get; set; }
}