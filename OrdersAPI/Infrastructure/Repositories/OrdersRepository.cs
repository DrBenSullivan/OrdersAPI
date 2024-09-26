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

		/// <summary>
		/// Gets all orders in database.
		/// </summary>
		/// <returns>A list of Orders or, an empty list if none found.</returns>
		public async Task<List<Order>> GetAllOrdersAsync()
		{
			List<Order>? ordersList = await _db.Orders
				.Include(o => o.Items)
				.ToListAsync();

			return ordersList ?? [];
		}

		/// <summary>
		/// Gets an order by the given GUID.
		/// </summary>
		/// <param name="orderId">The GUID to be searched for.</param>
		/// <returns>The order searched for.</returns>
		public async Task<Order?> GetOrderByIdAsync(Guid orderId)
		{
			Order? order = await _db.Orders
				.Include(o => o.Items)
				.FirstOrDefaultAsync(o => o.OrderId == orderId);	
			
			return order;
		}

		/// <summary>
		/// Adds the given order.
		/// </summary>
		/// <param name="order">The order to be added.</param>
		/// <returns>The added order.</returns>
		public async Task<Order> AddOrderAsync(Order order)
		{
			await _db.Orders.AddAsync(order);
			await _db.SaveChangesAsync();
			return order;
		}

		/// <summary>
		/// Updates the given Order.
		/// </summary>
		/// <param name="order">The Order to be updated.</param>
		/// <returns>The updated Order.</returns>
		public async Task<Order?> UpdateOrderAsync(Order order)
		{
			Order? existingOrder = await _db.Orders
				.Include(o => o.Items)
				.FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

			if (existingOrder == null)
			{
				return null;
			}

			existingOrder.OrderNumber = order.OrderNumber;
			existingOrder.OrderDate = order.OrderDate;
			existingOrder.CustomerName = order.CustomerName;
			existingOrder.TotalPrice = order.TotalPrice;

			_db.OrderItems.RemoveRange(existingOrder.Items);
			existingOrder.Items = order.Items;

			await _db.SaveChangesAsync();
			
			return existingOrder;
		}
	}
}
