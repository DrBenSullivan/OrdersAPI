using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.RepositoryInterfaces
{
	/// <summary>
	/// The class with methods for accessing the "OrderItems" table of the application's DbContext.
	/// </summary>
	public interface IOrderItemsRepository
	{
		/// <summary>
		/// Adds an OrderItem to the database.
		/// </summary>
		/// <param name="orderItem">The OrderItem to be added.</param>
		/// <returns>The added OrderItem.</returns>
		public Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);

		/// <summary>
		/// Retrieves a list of OrderItems from an existing Order in the database.
		/// </summary>
		/// <param name="orderId">The OrderId of the Order whose OrderItems are to be retrieved.</param>
		/// <returns>If the Order exists, a list of OrderItems. Otherwise, null.</returns>
		public Task<List<OrderItem>?> GetOrderItemsByOrderIdAsync(Guid orderId);

		/// <summary>
		/// Revieves an existing OrderItem using its OrderItemId from the database.
		/// </summary>
		/// <param name="orderItemId">The OrderItemID of the OrderItem to be retrieved.</param>
		/// <returns>If the OrderItem exists, the OrderItem. Otherwise, null.</returns>
		public Task<OrderItem?> GetOrderItemByIdAsync(Guid orderItemId);

		/// <summary>
		/// Updates an existing OrderItem in the database.
		/// </summary>
		/// <param name="orderItem">The updated OrderItem.</param>
		/// <returns>If successful, the updated OrderItem. Otherwise, null.</returns>
		public Task<OrderItem?> UpdateOrderItemAsync(OrderItem orderItem);

		/// <summary>
		/// Deletes an existing OrderItem from the database.
		/// </summary>
		/// <param name="orderItemId">The OrderItemId of the OrderItem to be deleted.</param>
		/// <returns>If successful, boolean 'true'. Otherwise, 'false'.</returns>
		public Task<bool> DeleteOrderItemAsync(Guid orderItemId);

		/// <summary>
		/// Checks if an OrderItem exists within the database using an OrderItemId.
		/// </summary>
		/// <param name="orderItemId">The OrderItemId of the OrderItem to be checked.</param>
		/// <returns>If the OrderItem exists, boolean 'true'. Otherwise, 'false'.</returns>
		public Task<bool> OrderItemExistsAsync(Guid orderItemId);
	}
}
