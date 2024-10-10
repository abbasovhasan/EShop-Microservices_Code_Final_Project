namespace Domain.Events;
public class OrderCompletedEvent
{
    public int OrderId { get; }
    public DateTime CompletedAt { get; }

    public OrderCompletedEvent(int orderId)
    {
        OrderId = orderId;
        CompletedAt = DateTime.UtcNow;
    }
}
