namespace OrdersAPI.Core.Interfaces.ServiceInterfaces.OrderItemsServiceInterfaces
{
	/// <summary>
	/// The class with methods to delete OrderItems from the database.
	/// </summary>
	public interface IOrderItemDeleterService
	{
		/// <summary>
		/// Checks if an OrderItem with given OrderItemId exists and passes it to OrderItemRepository for deletion.
		/// </summary>
		/// <param name="orderItemId">The OrderId of the OrderItem to be deleted.</param>
		/// <returns>If successfully deleted, Boolean 'true'. Otherwise, 'false'.</returns>
		public Task<bool> DeleteOrderItemAsync(Guid orderItemId);
	}
}
