using Domain.Enums;

namespace Domain.Models;

public class Order : BaseEntities
{
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public decimal TotalAmount { get; set; }

    // Order-OrderItem ilişkisi
    public ICollection<OrderItem> OrderItems { get; set; }

    // Customer ile ilişkisi
    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }

    // Payment ile ilişkisi
    public int? PaymentId { get; set; }
    public Payment Payment { get; set; }

    // OrderStatus Enum (Order Durumunu Takip Etmek İçin)
    public OrderStatus Status { get; set; }
}
