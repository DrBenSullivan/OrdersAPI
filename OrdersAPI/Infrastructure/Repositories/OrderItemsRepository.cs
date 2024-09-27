using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrdersAPI.Core.Interfaces.RepositoryInterfaces;
using OrdersAPI.Core.Models;
using OrdersAPI.Infrastructure.DatabaseContext;

namespace OrdersAPI.Infrastructure.Repositories
{
	///<inheritdoc/>
	public class OrderItemsRepository : IOrderItemsRepository
	{
		#region private readonly fields
		private readonly ILogger<OrderItemsRepository> _logger;
		private readonly ApplicationDbContext _db;
        #endregion

        #region constructors
		///<inheritdoc/>
        public OrderItemsRepository(ILogger<OrderItemsRepository> logger, ApplicationDbContext db)
        {
			_logger = logger;
            _db = db;
        }
		#endregion


		///<inheritdoc/>
		public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
		{
			_logger.LogInformation("{Method} reached with OrderItemId {OrderItemId}, adding to database...", nameof(AddOrderItemAsync), orderItem.OrderItemId);
			await _db.AddAsync(orderItem);
			await _db.SaveChangesAsync();
			return orderItem;
		}


		/// <inheritdoc/>
		public async Task<List<OrderItem>?> GetOrderItemsByOrderIdAsync(Guid orderId)
		{
			_logger.LogInformation("{Method} reached with OrderId {OrderId}, interrogating database...", nameof(GetOrderItemsByOrderIdAsync), orderId);

			return await _db.OrderItems
				.Where(o => o.OrderId == orderId)
				.ToListAsync();
		}


		///<inheritdoc/>
		public async Task<OrderItem?> GetOrderItemByIdAsync(Guid orderItemId)
		{
			_logger.LogInformation("{Method} reached with OrderItemId {OrderItemId}. Interrogating database...", nameof(GetOrderItemByIdAsync), orderItemId);

			return await _db.OrderItems
				.FirstOrDefaultAsync(o => o.OrderItemId == orderItemId);
		}


		///<inheritdoc/>
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


		///<inheritdoc/>
		public async Task<bool> DeleteOrderItemAsync(Guid orderItemId)
		{
			_logger.LogInformation("{Repository}.{Method} reached with OrderItemId {OrderItemId}. Interrogating database...", nameof(OrderItemsRepository), nameof(DeleteOrderItemAsync), orderItemId);

			OrderItem? existingItem = _db.OrderItems.Find(orderItemId);

			if (existingItem == null)
			{
				_logger.LogWarning("OrderItem with OrderItemId {OrderItemId} does NOT exist.", orderItemId);
			}
			else
			{
				_logger.LogInformation("OrderItem with OrderItemId {OrderItemId} exists! Deleting...", orderItemId);
				_db.OrderItems.Remove(existingItem);
				await _db.SaveChangesAsync();
			}

			return existingItem != null;
		}


		///<inheritdoc/>
		public async Task<bool> OrderItemExistsAsync(Guid orderItemId)
		{
			if (orderItemId == Guid.Empty)
			{
				throw new ArgumentException("OrderItemId must be provided", nameof(orderItemId));
			}

			_logger.LogInformation("{Repository}.{Method} Reached. Checking if Order with OrderId {OrderId} exists...", nameof(OrderItemsRepository), nameof(OrderItemExistsAsync), orderItemId);
			bool orderExists = await _db.OrderItems.AnyAsync(o => o.OrderItemId == orderItemId);

			if (orderExists)
			{
				_logger.LogInformation("Order exists.");
			}
			else
			{
				_logger.LogWarning("Order does NOT exist.");
			}

			return orderExists;
		}
	}
}
