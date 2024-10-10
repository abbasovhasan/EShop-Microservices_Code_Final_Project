namespace Domain.Events;

public class OrderCancelledEvent
{
    public int OrderId { get; }
    public DateTime CancelledAt { get; }

    public OrderCancelledEvent(int orderId)
    {
        OrderId = orderId;
        CancelledAt = DateTime.UtcNow;
    }
}