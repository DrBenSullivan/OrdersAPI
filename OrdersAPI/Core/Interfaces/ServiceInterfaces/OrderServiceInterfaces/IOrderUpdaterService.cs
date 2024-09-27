using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	/// <summary>
	/// The class with methods to update existing OrderItems within the database.
	/// </summary>
	public interface IOrderUpdaterService
	{
		/// <summary>
		/// Converts an UpdateOrderDTO into an Order and sends it to OrdersRepository to update the database.
		/// </summary>
		/// <param name="updateOrderDTO">The Order details to be updated as an UpdateOrderDTO.</param>
		/// <returns>If successful, the updated Order as an OrderResponseDTO. Otherwise, null.</returns>
		public Task<OrderResponseDTO?> UpdateOrderAsync(UpdateOrderDTO updateOrderDTO);
	}
}
