using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoriesAbstraction;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntities
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);

    // Eklenen metotlar:
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
    Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
}