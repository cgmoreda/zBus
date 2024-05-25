using zBus.Models;

namespace zBus.Data.Services
{
    public interface IOrderService
    {
         Order CreateOrder(Order order);
        Task<Order> GetOrderByIdAsync(int id);
    }
}
