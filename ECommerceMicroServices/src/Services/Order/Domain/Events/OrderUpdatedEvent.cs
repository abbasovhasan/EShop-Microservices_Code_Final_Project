namespace Domain.Events;

public class OrderUpdatedEvent
{
    public int OrderId { get; }
    public DateTime UpdatedAt { get; }

    public OrderUpdatedEvent(int orderId)
    {
        OrderId = orderId;
        UpdatedAt = DateTime.UtcNow;
    }
}
