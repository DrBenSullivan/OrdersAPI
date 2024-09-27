using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	/// <summary>
	/// The class with methods to retrieve OrderItems from the database.
	/// </summary>
	public interface IOrderGetterService
	{
		/// <summary>
		/// Retrieves all Orders.
		/// </summary>
		/// <returns>A list of all Orders as a list of OrderResponseDTOs.</returns>
		public Task<List<OrderResponseDTO>> GetAllOrdersAsync();

		/// <summary>
		/// Revieves an Order using its OrderId.
		/// </summary>
		/// <param name="orderId">The OrderId of the Order to be retrieved.</param>
		/// <returns>If the Order exists, the Order as an OrderResponseDTO. Otherwise, null.</returns>
		public Task<OrderResponseDTO?> GetOrderByIdAsync(Guid orderId);
	}
}
