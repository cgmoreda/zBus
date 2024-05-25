using zBus.Models;

namespace zBus.Data.Services
{
    public interface IOrderItemService
    {
        void CreateOrderItem(OrderItem orderItem);
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
    }
}
