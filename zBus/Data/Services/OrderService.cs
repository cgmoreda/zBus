using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zBus.Data;
using zBus.Data.Services;
using zBus.Models;

namespace zBus.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
             _context.SaveChanges();
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
              try
            {
                return await _context.Orders
             .Include(o => o.Items)
             .ThenInclude(oi => oi.Trip)
             .Include(o => o.User)
             .FirstOrDefaultAsync(o => o.Userid == id)?? new Order();
            }
            catch 
            {
                return new Order();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Trip)
                .Include(o => o.User)
                .ToListAsync();
        }

        
        
    }
}
