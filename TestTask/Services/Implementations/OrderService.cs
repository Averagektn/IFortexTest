using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }

        public Task<Order> GetOrder()
        {
            var mostValuableOrder = _dbContext.Orders
                .OrderByDescending(o => o.Price * o.Quantity)
                .FirstOrDefaultAsync();

            return mostValuableOrder;
        }

        public Task<List<Order>> GetOrders()
        {
            var orders = _dbContext.Orders.Where(o => o.Quantity > 10).ToListAsync();

            return orders;
        }
    }
}
