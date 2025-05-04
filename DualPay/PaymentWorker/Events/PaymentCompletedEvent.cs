namespace PaymentWorker.Events;

public class PaymentCompletedEvent
{
    public decimal Amount { get; set; }
    public string ToAccount { get; set; }
    public string FromAccount { get; set; }
    public string Status { get; set; }
    public DateTime CompletedAt { get; set; }
}