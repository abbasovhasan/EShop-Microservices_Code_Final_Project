namespace Domain.Exceptions;

public class OrderCancellationNotAllowedException : Exception
{
    public OrderCancellationNotAllowedException(int orderId)
        : base($"Order with ID {orderId} cannot be cancelled.")
    {
    }
}