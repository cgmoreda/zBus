using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zBus.Data;
using zBus.Data.Services;
using zBus.Models;

namespace zBus.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _context;

        public OrderItemService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateOrderItem(OrderItem orderItem)
        {
             _context.OrderItems.Add(orderItem);
             _context.SaveChanges();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _context.OrderItems
                .Include(oi => oi.Trip)
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == id)?? new OrderItem();
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Trip)
                .Include(oi => oi.Order)
                .ToListAsync();
        }
       
    }
}
