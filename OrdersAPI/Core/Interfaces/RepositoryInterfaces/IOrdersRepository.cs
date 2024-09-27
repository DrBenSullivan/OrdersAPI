using OrdersAPI.Core.Models;
using OrdersAPI.Core.Models.DTOs;

namespace OrdersAPI.Core.Interfaces.RepositoryInterfaces
{
	/// <summary>
	/// The class with methods for accessing the "Orders" table of the application's DbContext.
	/// </summary>
	public interface IOrdersRepository
	{
		/// <summary>
		/// Retrieves all Orders from the database.
		/// </summary>
		/// <returns>If Orders exist, a list of Orders. Otherwise, an empty list.</returns>
		public Task<List<Order>> GetAllOrdersAsync();

		/// <summary>
		/// Retrieves an existing Order from the database using its OrderId.
		/// </summary>
		/// <param name="orderId">The existing Order's OrderId to be retrieved.</param>
		/// <returns>The retrieved Order.</returns>
		public Task<Order?> GetOrderByIdAsync(Guid orderId);

		/// <summary>
		/// Adds an Order to the database.
		/// </summary>
		/// <param name="order">The Order to be added.</param>
		/// <returns>The added Order.</returns>
		public Task<Order> AddOrderAsync(Order order);

		/// <summary>
		/// Updates an existing Order in the database.
		/// </summary>
		/// <param name="order">The updated Order to be update the existing Order with.</param>
		/// <returns>The updated Order.</returns>
		public Task<Order?> UpdateOrderAsync(Order order);

		/// <summary>
		/// Deletes an existing Order in the database.
		/// </summary>
		/// <param name="orderId">The OrderId of the existing Order to be deleted.</param>
		/// <returns>Boolean 'true' if the Order was deleted. Otherwise, 'false'.</returns>
		public Task<bool> DeleteOrderByIdAsync(Guid orderId);

		/// <summary>
		/// Checks if an Order with the given OrderId exists in the database.
		/// </summary>
		/// <param name="orderId">The OrderId to be checked.</param>
		/// <returns>Boolean 'true' if an Order with the given OrderId exists. Otherwise, 'false'.</returns>
		public Task<bool> OrderExistsAsync(Guid? orderId);
	}
}
