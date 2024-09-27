using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Infrastructure.DatabaseContext;

namespace OrdersAPI.Infrastructure.Repositories
{
	public class OrderItemsRepository : IOrderItemsRepository
	{
		#region private readonly fields
		private readonly ILogger<OrderItemsRepository> _logger;
		private readonly ApplicationDbContext _db;
        #endregion

        #region constructors
        public OrderItemsRepository(ILogger<OrderItemsRepository> logger, ApplicationDbContext db)
        {
			_logger = logger;
            _db = db;
        }
		#endregion


		/// <summary>
		/// Adds an OrderItem to the database.
		/// </summary>
		/// <param name="orderItem">The OrderItem to be added.</param>
		/// <returns>The OrderItem</returns>
		public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
		{
			_logger.LogInformation("{Method} reached with OrderItemId {OrderItemId}, adding to database...", nameof(AddOrderItemAsync), orderItem.OrderItemId);
			await _db.AddAsync(orderItem);
			await _db.SaveChangesAsync();
			return orderItem;
		}


		/// <summary>
		/// Gets a list of OrderItems from the Order with the given orderId.
		/// </summary>
		/// <param name="orderId">The Order's GUID.</param>
		/// <returns>A list of OrderItems or, null if the Order does not exist.</returns>
		public async Task<List<OrderItem>?> GetOrderItemsByOrderIdAsync(Guid orderId)
		{
			_logger.LogInformation("{Method} reached with OrderId {OrderId}, interrogating database...", nameof(GetOrderItemsByOrderIdAsync), orderId);

			return await _db.OrderItems
				.Where(o => o.OrderId == orderId)
				.ToListAsync();
		}


		/// <summary>
		/// Gets an OrderItem by its GUID.
		/// </summary>
		/// <param name="orderItemId">The OrderItem's GUID</param>
		/// <returns>The OrderItem or, null if the OrderItem does not exist.</returns>
		public async Task<OrderItem?> GetOrderItemByIdAsync(Guid orderItemId)
		{
			_logger.LogInformation("{Method} reached with OrderItemId {OrderItemId}. Interrogating database...", nameof(GetOrderItemByIdAsync), orderItemId);

			return await _db.OrderItems
				.FirstOrDefaultAsync(o => o.OrderItemId == orderItemId);
		}


		/// <summary>
		/// Updates a given OrderItem.
		/// </summary>
		/// <param name="orderItem">The new OrderItem details.</param>
		/// <returns>The updated OrderItem if successful, otherwise null.</returns>
		public async Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem)
		{
			_logger.LogInformation("{Repository}.{Method} reached. Interrogating database...", nameof(OrderItemsRepository), nameof(GetOrderItemByIdAsync));

			OrderItem? existingItem = _db.OrderItems.Find(orderItem.OrderItemId);

			if (existingItem == null)
			{
				_logger.LogWarning("OrderItem with OrderItemId {OrderItemId} does NOT exist.", orderItem.OrderItemId);
			}
			else
			{
				_logger.LogInformation("OrderItem with OrderItemId {OrderItemId} exists! Updating...", orderItem.OrderItemId);
				existingItem.ProductName = orderItem.ProductName;
				existingItem.Quantity = orderItem.Quantity;
				existingItem.UnitPrice = orderItem.UnitPrice;
				existingItem.TotalPrice = orderItem.TotalPrice;
				await _db.SaveChangesAsync();
			}

			return existingItem;
		}
	}
}
