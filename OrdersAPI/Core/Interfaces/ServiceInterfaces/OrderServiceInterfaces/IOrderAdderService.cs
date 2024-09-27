using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderServiceInterfaces
{
	/// <summary>
	/// The class with methods to add Orders to the database.
	/// </summary>
	public interface IOrderAdderService
	{
		/// <summary>
		/// Converts an AddOrderDTO to an Order and sends it to OrdersRepository to be added to the database.
		/// </summary>
		/// <param name="addOrderDTO">The Order to be added as an AddOrderDTO.</param>
		/// <returns>The added Order as an OrderResponseDTO.</returns>
		public Task<OrderResponseDTO> AddOrderAsync(AddOrderDTO addOrderDTO);

	}
}
