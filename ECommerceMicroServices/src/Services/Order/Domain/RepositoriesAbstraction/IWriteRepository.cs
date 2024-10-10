using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoriesAbstraction;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntities
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

    // Eklenen metotlar:
    Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    Task AddOrderItemAsync(int orderId, OrderItem orderItem);
    Task RemoveOrderItemAsync(int orderId, int orderItemId);
}