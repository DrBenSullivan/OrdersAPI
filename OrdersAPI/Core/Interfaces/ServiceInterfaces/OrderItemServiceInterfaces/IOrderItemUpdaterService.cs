using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	/// <summary>
	/// The class with methods to update existing OrderItems within the database.
	/// </summary>
	public interface IOrderItemUpdaterService
	{
		/// <summary>
		/// Converts an UpdateOrderItemDTO into an OrderItem and sends it to OrderItemRepository to be update.
		/// </summary>
		/// <param name="updateOrderItemDTO">The updated OrderItem details as an UpdateOrderItemDTO.</param>
		/// <returns>If successful, the updated OrderItem as an OrderItemResponseDTO. Otherwise, null.</returns>
		public Task<OrderItemResponseDTO?> UpdateOrderItemAsync(UpdateOrderItemDTO updateOrderItemDTO);
	}
}
