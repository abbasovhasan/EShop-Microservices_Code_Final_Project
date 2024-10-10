namespace Domain.Exceptions;

public class OrderAlreadyCompletedException : Exception
{
    public OrderAlreadyCompletedException(int orderId)
        : base($"Order with ID {orderId} is already completed.")
    {
    }
}