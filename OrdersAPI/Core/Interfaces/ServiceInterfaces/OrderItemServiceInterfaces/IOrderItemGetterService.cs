using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	/// <summary>
	/// The class with methods to retrieve OrderItems from the database.
	/// </summary>
	public interface IOrderItemGetterService
	{
		/// <summary>
		/// Retrieves a list of OrderItems from an existing Order using the Order's OrderId.
		/// </summary>
		/// <param name="orderId">The OrderId of the existing Order whose OrderItems are to be retrieved.</param>
		/// <returns>If OrderItems are found, a list of OrderItemResponseDTOs or null, if no OrderItems are found.</returns>
		public Task<List<OrderItemResponseDTO>?> GetOrderItemsByOrderIdAsync(Guid orderId);

		/// <summary>
		/// Retrieves an OrderItem using its OrderItemId.
		/// </summary>
		/// <param name="orderItemId">The OrderItemId of the OrderItem to be retrieved.</param>
		/// <returns>If the OrderItem exists, the OrderItem as an OrderItemResponseDTO. Otherwise, null.</returns>
		public Task<OrderItemResponseDTO?> GetOrderItemByIdAsync(Guid orderItemId);
	}
}
