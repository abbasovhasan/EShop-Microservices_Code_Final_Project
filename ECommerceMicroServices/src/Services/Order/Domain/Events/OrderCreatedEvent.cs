namespace Domain.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; }
    public DateTime CreatedAt { get; }

    public OrderCreatedEvent(int orderId)
    {
        OrderId = orderId;
        CreatedAt = DateTime.UtcNow;
    }
}