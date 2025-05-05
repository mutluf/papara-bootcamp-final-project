namespace PaymentWorker.Events;

public class ExpenseApprovedEvent
{
    public decimal Amount { get; set; }
    public string ToAccount { get; set; }
    public string FromAccount { get; set; }
    public int ExpenseId { get; set; }
}