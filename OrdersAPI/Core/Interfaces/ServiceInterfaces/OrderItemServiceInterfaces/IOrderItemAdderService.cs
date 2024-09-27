using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	/// <summary>
	/// The class with methods to add OrderItems to the database.
	/// </summary>
	public interface IOrderItemAdderService
	{
		/// <summary>
		/// Converts an AddOrderItemDTO to an OrderItem and sends it to OrderItemRepository to be added.
		/// </summary>
		/// <param name="addOrderItemDTO">The OrderItem to be added as an AddOrderItemDTO.</param>
		/// <returns>If successful, the added OrderItem as an OrderItemResponseDTO. Otherwise, null.</returns>
		public Task<OrderItemResponseDTO?> AddOrderItemAsync(AddOrderItemDTO addOrderItemDTO);
	}
}
