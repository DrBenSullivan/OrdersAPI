using Microsoft.EntityFrameworkCore;
using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Infrastructure.DatabaseContext;

namespace OrdersAPI.Infrastructure.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		# region private readonly fields
		private readonly ApplicationDbContext _db;
        # endregion

        # region constructors
        public OrdersRepository(ApplicationDbContext db)
        {
            _db = db;
        }
		#endregion

		public async Task<List<Order>> GetAllOrdersAsync()
		{
			return _db.Orders.Any()
				? await _db.Orders.Include(o => o.Items).ToListAsync()
				: [];
		}

		public async Task<Order?> GetOrderByIdAsync(Guid id)
		{
			return await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == id);				
		}

		public async Task<Order> AddOrderAsync(Order order)
		{
			await _db.Orders.AddAsync(order);
			await _db.SaveChangesAsync();
			return order;
		}
	}
}
